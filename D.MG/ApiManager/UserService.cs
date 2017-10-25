using ApiManager.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManager
{
    public class UserService
    {
        private DbSet<User> _dbSet;
        private DataContext _ctx;

        public UserService(DataContext ctx)
        {
            _dbSet = ctx.Set<User>();
            _ctx = ctx;
        }

        public List<User> InsertUsers()
        {
            var users = new List<User>();
            string[] headers;
            string[] fields;
            int counter = 0;
            var fileStream = File.ReadAllLines(@"C:\git\DebtManagment\assets\CitizenDebts_1M_3.txt");

            var firstline = fileStream[0];
            headers = firstline.Split(";");
            try
            {
                for (var j = 1; j < fileStream.Length; j++)
                {

                    User user = new User();
                    Bill debt = new Bill();
                    user.Password = AuthProvider.PasswordHasher.HashPassword("123456");
                    fields = fileStream[j].Split(";");
                    user.Name = fields[1];
                    user.Lastname = fields[2];
                    user.Address.Street = fields[5];
                    user.Vat = fields[0];
                    user.Address.County = fields[6];
                    user.Email = fields[3];
                    user.Mobile = fields[4];
                    debt.Amount = Double.Parse(fields[9]);
                    debt.Description = fields[8];
                    debt.DateDue = ConvertDate(fields[10]);
                    debt.Bill_Id = fields[7];

                    var exists = users.FindIndex(x => x.Vat == user.Vat);

                    if (exists > -1)
                    {
                        users[exists].Bills.Add(debt);
                    }
                    else
                    {
                        user.Bills.Add(debt);
                        users.Add(user);
                    }

                }
                _dbSet.AddRange(users);
                _ctx.SaveChanges();
                _ctx.Dispose();
            }
            catch (Exception ex)
            {
                var error = counter;
                var _ex = ex;
            }

            return users;
        }

        private static DateTime ConvertDate(string date)
        {
            var dateformat = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
            return DateTime.Parse(dateformat);
        }

        public async Task<User> GetUser(string id)
        {
            var user = await _dbSet.Include(x => x.Address).SingleOrDefaultAsync(s => s.Id == id);

            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Vat == username);

            return user;
        }

        public async Task<Counter> GetUserCounters(string userId)
        {
            var counter = new Counter
            {
                Bills = await _ctx.Set<Bill>().Where(x => x.UserId == userId).CountAsync(),
                Payments = await _ctx.Set<Payment>().Include(i => i.Bill).Where(x => x.Bill.UserId == userId).CountAsync(),
                Settlements = await _ctx.Set<Bill>().Where(x => x.UserId == userId && x.SettlementId != null).CountAsync()
            };

            return counter;
        }

    }
}
