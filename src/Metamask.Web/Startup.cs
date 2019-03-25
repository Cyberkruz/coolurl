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
                    opts.UseSqlServer(connectionStrings.SqlDatabaseConnection,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(3,
                                TimeSpan.FromSeconds(5),
                                policies.GetSqlRetryCodes());
                        });
                });

            services.AddScoped<IPageMaskRepository, SqlPageMaskRepository>();

            services.AddAutoMapper(typeof(DtoProfile).Assembly,
                    typeof(ModelProfile).Assembly);

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
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
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
