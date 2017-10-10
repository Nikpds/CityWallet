using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlContext;
using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext.Repos;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //  .AddJsonFile("appsettings.json")
            //  .Build();
            //Configuration.GetConnectionString("DefaultConnection")

            services.AddDbContext<DataContext>(options => options.UseSqlServer("Server=FreeNet-1;Database=qualco4;Integrated Security=true;"));
            services.AddTransient<UnitOfWork>();
            //services.AddDbContext<DataContext>(options => options.UseSqlServer("Server=tcp:qualco4codingschool.database.windows.net,1433;Initial Catalog=qualco4;Persist Security Info=False;User ID=scrummaster;Password=1qaz+1qaz;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
