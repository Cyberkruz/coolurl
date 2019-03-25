using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metamask.Web.Configuration
{
    public static class RewriteOptionsExtensions
    {
        /// <summary>
        /// Extension fot adding AzureRedirectRule class to the
        /// Rewrite options to quickly configure Azurewebsites redirection.
        /// </summary>
        /// <param name="options">The RewriteOptions configured in Startup.</param>
        /// <param name="host">The hostname (including http/https) to redirect to.</param>
        /// <returns>The configured instance of itself for chaining.</returns>
        public static RewriteOptions AddAzureHostnameRedirect(
            this RewriteOptions options, string host)
        {
            options.Rules.Add(new AzureRedirectRule(host));
            return options;
        }
    }
}
