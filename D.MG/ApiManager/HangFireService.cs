using AuthProvider;
using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DMG.Services
{
    public interface IHangFireService
    {
        void DeleteDatabase();
        void InsertNewData();
    }

    public class HangFireService : IHangFireService
    {
        private DataContext _ctx;

        public HangFireService(DataContext ctx)
        {
            _ctx = ctx;
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

        public void InsertNewData()
        {
            var users = new List<User>();
            var bills = new List<Bill>();
            var uniqueUsers = new List<string>();
            string[] headers;
            string[] fields;
            int counter = 0;
            var fileStream = File.ReadAllLines(@"C:\Users\Nickos\Source\github\DebtManagment\assets\CitizenDebts_1M_3.txt");
            var psw = PasswordHasher.HashPassword("123");
            var firstline = fileStream[0];
            headers = firstline.Split(";");
            try
            {
                for (var j = 1; j < fileStream.Length; j++)
                {
                    User user = new User();
                    Bill debt = new Bill();
                    user.Password = psw;
                    fields = fileStream[j].Split(";");
                    user.Name = fields[1];
                    user.Lastname = fields[2];
                    user.AddressInfo.Street = fields[5];
                    user.Vat = fields[0];
                    user.AddressInfo.County = fields[6];
                    user.Email = fields[3];
                    user.Mobile = fields[4];
                    user.FirstLogin = true;
                    user.LastUpdate = DateTime.Now;
                    debt.Amount = Double.Parse(fields[9]);
                    debt.Description = fields[8];
                    debt.DateDue = ConvertDate(fields[10]);
                    debt.Bill_Id = fields[7];
                    debt.LastUpdate = DateTime.Now;

                    var index = uniqueUsers.FindIndex(i => i == user.Vat);
                    if (index > -1)
                    {
                        users[index].Bills.Add(debt);
                    }
                    else
                    {
                        uniqueUsers.Add(user.Vat);
                        user.Bills.Add(debt);
                        users.Add(user);
                    }
                }
                _ctx.Set<User>().AddRange(users);
                _ctx.SaveChanges();
                _ctx.Dispose();
            }
            catch (Exception ex)
            {
                var error = counter;
                var _ex = ex;
            }
        }


        public void InsertDataFromCounty()
        {
            var users = new List<User>();
            var fileStream = File.ReadAllLines(@"C:\Users\Nickos\Source\github\DebtManagment\assets\CitizenDebts_3M_3.txt");

            var listToImport = File.ReadLines(@"C:\Users\Nickos\Source\github\DebtManagment\assets\CitizenDebts_3M_3.txt").Skip(1)
                          .Select(line => line.Split(';'))
                          .ToList();

            var distinctusers = (from item in listToImport
                                 group item by item[0] into groups
                                 select groups.First()).ToList();

            var usersInDb = _ctx.Set<User>().ToList();
            string[] headers;
            string[] fields;
            var psw = PasswordHasher.HashPassword("123");
            var firstline = fileStream[0];
            headers = firstline.Split(";");
            try
            {
                Console.WriteLine($"{DateTime.Now}   Number of Users in db: {usersInDb.Count().ToString()} Number of Users in file: {distinctusers.Count().ToString()}");
                var userlist = (from ca in distinctusers
                                select new User
                                {
                                    Vat = ca[0],
                                    Name = ca[1],
                                    Lastname = ca[2],
                                    Email = ca[3],
                                    Password = "123",
                                    Mobile = ca[4],
                                    AddressInfo = new Address { County = ca[5], Street = ca[6] },
                                    FirstLogin = false,
                                    LastUpdate = DateTime.Now
                                }).ToList();
                Console.WriteLine($"{DateTime.Now}   userlist finished");
                var usersToInsert = (from u in userlist
                                     join _u in usersInDb on u.Vat equals _u.Vat into toInsert
                                     from _user in toInsert.DefaultIfEmpty()
                                     where _user == null
                                     select new User
                                     {
                                         Vat = u.Vat,
                                         Name = u.Name,
                                         Lastname = u.Lastname,
                                         Email = u.Email,
                                         Password = Sha256_hash("123"),
                                         Mobile = u.Mobile,
                                         AddressInfo = u.AddressInfo,
                                         FirstLogin = false,
                                         LastUpdate = u.LastUpdate
                                     }).ToList();
                Console.WriteLine($"{DateTime.Now}   usertoInsert finished");

                foreach (var item in usersToInsert)
                {
                    _ctx.Set<User>().Add(item);
                }
                Console.WriteLine($"{DateTime.Now}   all adds where finished");
                // _ctx.Set<User>().AddRange(usersToInsert);

                _ctx.SaveChanges();
                //var usersInDb2 = _ctx.Set<User>().ToList();
                //var billsforuser = (from item in listToImport
                //                    join ff in usersInDb2 on item[0] equals ff.Vat
                //                    select new { item, ff.Id }).ToList();

                //var allbills = (from bill in billsforuser
                //                select new Bill
                //                {
                //                    Bill_Id = bill.item[7],
                //                    Description = bill.item[8],
                //                    Amount = float.Parse(bill.item[9].Replace(',', '.'), CultureInfo.InvariantCulture.NumberFormat),
                //                    DateDue = DateTime.ParseExact(bill.item[10], "yyyyMMdd", CultureInfo.InvariantCulture),
                //                    UserId = bill.Id
                //                }).ToList();

                //_ctx.Set<Bill>().AddRange(allbills.Take(200000));
                //_ctx.SaveChanges();


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
    }
}
