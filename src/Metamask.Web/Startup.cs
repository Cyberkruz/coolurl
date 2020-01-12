using AutoMapper;
using Metamask.Data;
using Metamask.Data.Sql;
using Metamask.Web.Configuration;
using Metamask.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Hosting;

namespace Metamask.Web
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
            // Connection Strings
            var connectionStringsSection = Configuration.GetSection("ConnectionStrings");
            var connectionStrings = connectionStringsSection.Get<ConnectionStrings>();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            // Options
            services.Configure<ConnectionStrings>(connectionStringsSection);
            services.Configure<AppSettings>(appSettingsSection);

            var policies = new Policies();
            services.AddSingleton(policies);

            // Configure Entity Framework
            services
                .AddDbContext<MetamaskSqlContext>(opts =>
                {
                    opts.UseNpgsql(connectionStrings.SqlDatabaseConnection);
                });

            services.AddScoped<IPageMaskRepository, SqlPageMaskRepository>();

            services.AddAutoMapper(typeof(DtoProfile).Assembly,
                    typeof(ModelProfile).Assembly);

            services.AddControllersWithViews();
        }
        
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            IOptions<AppSettings> appSettings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var options = new RewriteOptions();
            options.AddAzureHostnameRedirect(appSettings.Value.Hostname);
            app.UseRewriter(options);
    
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
