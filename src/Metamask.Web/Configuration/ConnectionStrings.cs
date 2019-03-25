namespace Metamask.Web.Configuration
{
    /// <summary>
    /// Class mapping json to a strongly typed object in the
    /// appsettings.json (ConnectionStrings section).
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// Database connection string used for the ServiceSqlContext.
        /// </summary>
        public string SqlDatabaseConnection { get; set; }
    }
}
