using Metamask.Data.Sql;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Metamask.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                var config = (IConfiguration)host.Services.GetService(typeof(IConfiguration));

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();

                Log.Information("Initializing database");
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    await SqlContextInitializer.Initialize(services);
                }
                Log.Information("Database initialized");

                Log.Information("Starting webhost");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectidly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((context, config) =>
                        {
                            config.AddJsonFile("settings/appsettings.docker.json", optional: true);
                        })
                        .UseStartup<Startup>()
                        .UseSerilog();
                });
    }
}
