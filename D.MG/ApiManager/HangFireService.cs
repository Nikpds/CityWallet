using DMG.Services.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using ServiceStack;
using ServiceStack.Text;
using SqlContext;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Z.EntityFramework.Extensions;

namespace DMG.Services
{
    public interface IHangFireService
    {
        void DeleteDatabase();
        void InsertData();
        void ExportData();
    }

    public class HangFireService : IHangFireService
    {
        private DataContext _ctx;
        public HangFireService(DataContext ctx)
        {
            _ctx = ctx;
            EntityFrameworkManager.ContextFactory = context => { return _ctx; };
        }

        public void DeleteDatabase()
        {
            try
            {
                _ctx.Database.ExecuteSqlCommand("Delete From [qualco4].[dbo].[Bill]");
                _ctx.Database.ExecuteSqlCommand("Delete From [qualco4].[dbo].[Settlement]");
                _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }

        public void InsertData()
        {
            var filepath = @"D:\git\DebtManagment\assets\CitizenDebts_3M_3.txt";
            var dataForImport = File.ReadLines(filepath)
                               .Skip(1)
                               .Select(line => line.Split(';'))
                               .ToList();

            var distinctUsers = (from data in dataForImport
                                 group data by data[0] into groups
                                 select groups.First()).ToList();

            var dbUsers = _ctx.Set<User>().ToList();
            try
            {
                Console.WriteLine($"{DateTime.Now}   Number of Users in db: {dbUsers.Count().ToString()} Number of Users in file: {distinctUsers.Count().ToString()}");
                var userlist = (from user in distinctUsers
                                select new User
                                {
                                    Vat = user[0],
                                    Name = user[1],
                                    Lastname = user[2],
                                    Email = user[3],
                                    Password = Sha256_hash("123"),//in production must be replaced with RandomString(6)
                                    Mobile = user[4],
                                    AddressInfo = new Address { County = user[5], Street = user[6] },
                                    FirstLogin = false,
                                    LastUpdate = DateTime.Now
                                }).ToList();

                Console.WriteLine($"{DateTime.Now}   userlist finished");

                var usersToInsert = (from u in userlist
                                     join _u in dbUsers on u.Vat equals _u.Vat into toInsert
                                     from _user in toInsert.DefaultIfEmpty()
                                     where _user == null
                                     select new User
                                     {
                                         Id = Guid.NewGuid().ToString(),
                                         Vat = u.Vat,
                                         Name = u.Name,
                                         Lastname = u.Lastname,
                                         Email = u.Email,
                                         Password = u.Password,
                                         Mobile = u.Mobile,
                                         AddressInfo = u.AddressInfo,
                                         FirstLogin = u.FirstLogin,
                                         LastUpdate = u.LastUpdate
                                     }).ToList();
                Console.WriteLine($"{DateTime.Now}   usertoInsert finished, Starts removing");

                usersToInsert.RemoveAll(x => x.Vat == null);

                Console.WriteLine($"{DateTime.Now}   removing corrupted users finished");

                if (usersToInsert.Count > 0)
                {
                    _ctx.BulkInsert(usersToInsert);
                    Console.WriteLine($"{DateTime.Now}   all users finished bulk insert where finished");
                    Console.WriteLine($"{DateTime.Now}   Start Saving...");
                    _ctx.SaveChanges();
                    Console.WriteLine($"{DateTime.Now}   Saving finished");
                }

                var dbUsersNew = _ctx.Set<User>().ToList();

                var billsforuser = (from item in dataForImport
                                    join ff in dbUsersNew on item[0] equals ff.Vat
                                    select new { item, ff.Id }).ToList();

                var allbills = (from bill in billsforuser
                                select new Bill
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Bill_Id = bill.item[7],
                                    Description = bill.item[8],
                                    Amount = decimal.Parse(bill.item[9].Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat),
                                    DateDue = DateTime.ParseExact(bill.item[10], "yyyyMMdd", CultureInfo.InvariantCulture),
                                    UserId = bill.Id,
                                    LastUpdate = DateTime.Now
                                }).ToList();

                Console.WriteLine($"{DateTime.Now}   all bills finished bulk insert starts.. bills to insert: " + allbills.Count);
                allbills.RemoveAll(x => x.Bill_Id == null || x.Id == null);

                Console.WriteLine($"{DateTime.Now}   removing corrupted bills finished");
                _ctx.BulkInsert(allbills);
                Console.WriteLine($"{DateTime.Now}   Start Saving...");
                _ctx.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        private static DateTime ConvertDate(string date)
        {
            var dateformat = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
            return DateTime.Parse(dateformat);
        }

        public static String Sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void ExportData()
        {
            var exportPath = @"C:\Users\Nperperidis\";
            CsvConfig.ItemSeperatorString = ";";
            var payments = _ctx.Set<Payment>().Include(i => i.Bill).ToList();
            var settlements = _ctx.Set<Settlement>().Include(x => x.SettlementType).Include(i => i.Bills).ThenInclude(u => u.User).ToList();

            var d = DateTime.Now.AddDays(-1);
            var filename = d.Year + "" + d.Month + d.Day;
            var paymentsForExport = (from pay in payments
                                     select new PaymentExport
                                     {
                                         BILL_ID = pay.Bill.Bill_Id,
                                         TIME = pay.PaidDate.ToUniversalTime(),
                                         AMOUNT = pay.Bill.Amount,
                                         METHOD = pay.Method
                                     }).ToList();

            var settlementsForExport = (from s in settlements
                                        select new SettlementExport
                                        {
                                            VAT = s.Bills.FirstOrDefault().User.Vat,
                                            BILLS = ConvertBills(s.Bills.ToList()),
                                            DOWNPAYMENT = s.Downpayment,
                                            INSTALLMENTS = s.Installments,
                                            TIME = s.RequestDate,
                                            INTEREST = s.SettlementType.Interest

                                        }).ToList();

            var paymentsTxt = CsvSerializer.SerializeToCsv(paymentsForExport);
            var settlementsTxt = CsvSerializer.SerializeToCsv(settlementsForExport);

            File.AppendAllText(exportPath + "PAYMENTS_" + filename + ".txt", paymentsTxt);
            File.AppendAllText(exportPath + "SETTLEMENTS_" + filename + ".txt", settlementsTxt);


        }

        private static string ConvertBills(List<Bill> bills)
        {
            var billIdString = "";
            var users = bills.FirstOrDefault();
            for (var i = 0; i < bills.Count; i++)
            {
                billIdString = billIdString + bills[i].Bill_Id;
                if (i != bills.Count - 1)
                {
                    billIdString = billIdString + ",";
                }
            }
            return billIdString;
        }
    }
}
