using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Metamask.Data.Sql
{
    /// <summary>
    /// Used by the dotnet ef command to automatically scaffold
    /// the SQL database.
    /// </summary>
    public class DesignTimeSqlContextFactory
        : IDesignTimeDbContextFactory<MetamaskSqlContext>
    {
        /// <summary>
        /// Builds a ServiceSqlContext by looking at app settings json file and 
        /// uses the connection string.
        /// </summary>
        /// <param name="args">Arguments passed in from the ef pipeline.</param>
        /// <returns>A new ServiceSqlContext for scaffolding.</returns>
        public MetamaskSqlContext CreateDbContext(string[] args)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var projectName = Path.GetFileName(projectDir);
            var webProjectPath = Path.Combine(projectDir, $"../{projectName}");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(webProjectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<MetamaskSqlContext>();
            var connectionString = configuration.GetConnectionString("SqlDatabaseConnection");

            builder.UseNpgsql(connectionString);

            return new MetamaskSqlContext(builder.Options);
        }
    }
}
