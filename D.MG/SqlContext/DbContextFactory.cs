using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlContext
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(@"C:\git\DebtManagment\D.MG\WebApp\")
              .AddJsonFile("appsettings.json")
              .Build();

            var connection = configuration["ConnectionStrings:DefaultConnection"];
           
            builder.UseSqlServer(connection);

            return new DataContext(builder.Options);
        }
    }
}
