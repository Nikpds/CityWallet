using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlContext;
using Microsoft.EntityFrameworkCore;
using AuthProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //  .SetBasePath(@"D:\git\DebtManagment\D.MG\Web.Api\")
            //  .AddJsonFile("appsettings.json")
            //  .Build();
            //var test = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DataContext>(options => options.UseSqlServer("Server=DESKTOP-L9O20VR;Database=qualco4;Integrated Security=true;"));
            services.AddTransient<UnitOfWork>();

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();

            services.AddCors();
            //services.AddDbContext<DataContext>(options => options.UseSqlServer("Server=tcp:qualco4codingschool.database.windows.net,1433;Initial Catalog=qualco4;Persist Security Info=False;User ID=scrummaster;Password=1qaz+1qaz;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));


            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters();

                cfg.TokenValidationParameters.ValidIssuer = Configuration["Tokens:Issuer"];
                cfg.TokenValidationParameters.ValidAudience = Configuration["Tokens:Issuer"];
                cfg.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));



            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
