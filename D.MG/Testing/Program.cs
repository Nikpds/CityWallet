using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SqlContext;
using Models;
using ApiManager;
using System.Threading.Tasks;

namespace Testing
{
    public class Program
    {

        static void Main(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseSqlServer("");

            var _db = new UnitOfWork(new DataContext(builder.Options));
            var users = UserManager.InsertUsers();

            var count = users.Count;

            _db.UserRepository.InsertMany(users);
            _db.Save();

        }
    }

}
