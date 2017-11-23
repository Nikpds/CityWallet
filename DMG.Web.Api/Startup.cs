using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlContext;
using Microsoft.EntityFrameworkCore;
using ApiManager;
using AuthProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using DMG.Services;
using Hangfire;
using System.IO;

namespace DMG.Web.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokensKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(new Guid().ToString()));
            auth = new AuthenticationProvider(_tokensKey);
        }

        public readonly IConfiguration Configuration;
        private readonly AuthenticationProvider auth;
        private readonly SymmetricSecurityKey _tokensKey;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.Configure<SmtpOptions>(Configuration.GetSection("SMTP"));

            services.AddTransient<IEmailProvider, EmailProvider>();
            services.AddTransient<IHangFireService, HangFireService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ISettlementService, SettlementService>();

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();
            services.AddSingleton<IAuthenticationProvider>(p => auth);

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

            services.AddCors();
            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc().AddJsonOptions(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHangFireService ihsrv)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            //app.UseHangfireServer();

            //BackgroundJob.Schedule(() => ihsrv.ExportData(), TimeSpan.FromDays(1));
            //BackgroundJob.Schedule(() => ihsrv.DeleteDatabase(), TimeSpan.FromDays(1));
            //BackgroundJob.Schedule(() => ihsrv.InsertData(), TimeSpan.FromDays(1));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
