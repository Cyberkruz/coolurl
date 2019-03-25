namespace Metamask.Web.Configuration
{
    /// <summary>
    /// Class mapping json to a strongly typed object in the
    /// appsettings.json (AppSettings section).
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Links are dynamically generated. Specify the hostname
        /// so they backlink to the correct location.
        /// </summary>
        public string Hostname { get; set; }
    }
}
