﻿using Microsoft.EntityFrameworkCore;
using SqlContext;
using ApiManager;
using System.IO;
using Microsoft.Extensions.Configuration;
using Models;

namespace Testing
{
    public class Program
    {

        static void Main(string[] args)
        {

            string path = Directory.GetCurrentDirectory().Replace("Testing", "WebApp");
            var builder = new DbContextOptionsBuilder<DataContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(@path)
              .AddJsonFile("appsettings.json")
              .Build();
            var connection = configuration["ConnectionStrings:DefaultConnection"];
            builder.UseSqlServer(connection);

            var _db = new DataContext(builder.Options);
            var srv = new UserService(_db);
            var srv1 = new SettlementService(_db);
            srv1.CreateSettlementTypes();
           // var users = srv.InsertUsers();


        }
    }

}
