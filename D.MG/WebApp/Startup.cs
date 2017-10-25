using Microsoft.Extensions.DependencyInjection;
using SqlContext;
using Microsoft.EntityFrameworkCore;
using AuthProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using SqlContext.Repos;
using ApiManager;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var tokensKey = new Guid().ToString();
            _tokensKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokensKey));
            auth = new AuthenticationProvider(_tokensKey);
            
        }

        public IConfiguration Configuration { get; }

        private readonly SymmetricSecurityKey _tokensKey;

        private readonly AuthenticationProvider auth;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddScoped<BillService>();
            services.AddScoped<UserService>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ISettlementRepository, SettlementRepository>();

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();
            services.AddSingleton<IAuthenticationProvider>(p => auth);

            services.AddCors();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = _tokensKey
                };
            });

            services.AddMvc().AddJsonOptions(
                 options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
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
