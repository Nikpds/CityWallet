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
using ApiManager;
using Swashbuckle.AspNetCore.Swagger;
using Hangfire;
using DMG.Services;
using System.IO;

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

        public readonly IConfiguration Configuration;
        private readonly SymmetricSecurityKey _tokensKey;
        private readonly AuthenticationProvider auth;

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.Configure<SmtpOptions>(Configuration.GetSection("SMTP"));
            services.AddTransient<IEmailProvider, EmailProvider>();
            services.AddScoped<IBillService, BillService>();
            services.AddTransient<IHangFireService, HangFireService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISettlementService, SettlementService>();
            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();
            services.AddSingleton<IAuthenticationProvider>(p => auth);

            services.AddCors();
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionStrings:DefaultConnection"]));
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

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }); });
            services.AddMvc().AddJsonOptions(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app,IHostingEnvironment env,IHangFireService ihsrv)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                StatsPollingInterval = 60000
            });

            app.UseHangfireServer();

            //RecurringJob.AddOrUpdate(() => ihsrv.ExportData(), Cron.Daily(00, 30));
            //RecurringJob.AddOrUpdate(() => ihsrv.DeleteDatabase(), Cron.Daily(01, 30));
            //RecurringJob.AddOrUpdate(() => ihsrv.InsertDataFromCounty(), Cron.Yearly());

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404
                    && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvc();
        }
    }
}
