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
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //  .AddJsonFile("appsettings.json")
            //  .Build();
            var connection = @"Server=DESKTOP-L9O20VR;Database=qualco4;Integrated Security=true;";
            //var connection = @"Server=tcp:qualco4codingschool.database.windows.net,1433;Initial Catalog=qualco4;Persist Security Info=False;User ID=scrummaster;Password=1qaz+1qaz;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=130;";
            builder.UseSqlServer(connection);

            return new DataContext(builder.Options);
        }
    }
}
