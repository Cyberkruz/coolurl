using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Metamask.Data.Sql
{
    /// <summary>
    /// Initializes, migrates, and seeds the 
    /// database. If using Asp.Net Core this
    /// should be used in the Program.cs to setup the database
    /// in the instance it doesn't exist.
    /// </summary>
    public class SqlContextInitializer
    {
        /// <summary>
        /// Performs the actual actions of using migrations to setup
        /// the database and seeding with test data if required.
        /// </summary>
        /// <param name="services">Core serviceprovider from dependancy
        /// injection that is used to instantiate the contexts.</param>
        /// <returns>Empty task because it is async.</returns>
        public static async Task Initialize(IServiceProvider services)
        {
            MetamaskSqlContext context =
                services.GetRequiredService<MetamaskSqlContext>();

            await context.Database.MigrateAsync();
        }
    }
}
