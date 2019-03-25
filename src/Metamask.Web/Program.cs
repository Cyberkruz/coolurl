using Metamask.Data.Sql;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Metamask.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                IWebHost host = BuildWebHost(args);
                IConfiguration config = (IConfiguration)host.Services.GetService(typeof(IConfiguration));

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();

                Log.Information("Initializing database");
                using (IServiceScope scope = host.Services.CreateScope())
                {
                    IServiceProvider services = scope.ServiceProvider;
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

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
