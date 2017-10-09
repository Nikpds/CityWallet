using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SqlContext;
using Models;

namespace Testing
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connection = @"Data Source=c:\qualco4.db;";
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlite(connection);
            using (var db = new DataContext(builder.Options))
            {
                db.Users.Add(new User { Name = "NIkos", Lastname = "perpe" });
                db.SaveChanges();
                foreach (var user in db.Users)
                {
                    Console.WriteLine(user.Name);
                }
            }
        }

    }

}
