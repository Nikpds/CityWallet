using ApiManager;
using AuthProvider;
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
        private IEmailProvider _smtp;
        public HangFireService(DataContext ctx, IEmailProvider smtp)
        {
            _smtp = smtp;
            _ctx = ctx;
            EntityFrameworkManager.ContextFactory = context => { return _ctx; };
        }

        public void DeleteDatabase()
        {
            try
            {
                _ctx.Database.ExecuteSqlCommand("Delete From [CityWallet].[dbo].[Bill]");
                _ctx.Database.ExecuteSqlCommand("Delete From [CityWallet].[dbo].[Settlement]");
                _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertData()
        {
            try
            {
                var filepath = @"C:\Users\Nickos\Downloads\CitizenDebts_1M_3.txt";
                var dataForImport = File.ReadLines(filepath)
                                   .Skip(1)
                                   .Select(line => line.Split(';'))
                                   .ToList();

                var distinctUsers = (from data in dataForImport
                                     group data by data[0] into groups
                                     select groups.First()).ToList();

                var dbUsers = _ctx.Set<User>().ToList();

                var userlist = (from user in distinctUsers
                                select new User
                                {
                                    Vat = user[0],
                                    Name = user[1],
                                    Lastname = user[2],
                                    Email = user[3],
                                    Password = RandomString(6),
                                    Mobile = user[4],
                                    AddressInfo = new Address { County = user[5], Street = user[6] },
                                    FirstLogin = true,
                                    LastUpdate = DateTime.Now
                                }).ToList();

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

                var address = (from _addr in usersToInsert
                               select new Address
                               {
                                   County = _addr.AddressInfo.County,
                                   Street = _addr.AddressInfo.Street,
                                   UserId = _addr.Id
                               }).ToList();

                for (var i = 0; i < address.Count; i++)
                {
                    address[i].Id = (i + 1);
                }

                usersToInsert.RemoveAll(x => x.Vat == null);

                if (usersToInsert.Count > 0)
                {
                    _ctx.BulkInsert(usersToInsert);
                    _ctx.BulkInsert(address);
                    _ctx.SaveChanges();

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

                allbills.RemoveAll(x => x.Bill_Id == null || x.Id == null);

                _ctx.BulkInsert(allbills);
                _ctx.SaveChanges();

                foreach (var user in usersToInsert)
                {
                    _smtp.NewAccount(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
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
