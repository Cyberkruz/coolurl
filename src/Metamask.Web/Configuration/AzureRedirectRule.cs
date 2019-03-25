using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metamask.Web.Configuration
{
    /// <summary>
    /// If hosting in Azure this rule redirects a request
    /// to the Azure url hostname to the hostname specified.
    /// Good for redirecting people from mysite.azurewebsites.net
    /// to a specific site so users don't have two publicly exposed
    /// endpoints.
    /// </summary>
    public class AzureRedirectRule : IRule
    {
        private readonly string _host;

        public AzureRedirectRule(string host)
        {
            _host = host;
        }

        public void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            if (!req.Host.Host.Contains("azurewebsites.net", 
                StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }
            var url = string.Format($"{_host.TrimEnd('/')}{req.Path}");
            var response = context.HttpContext.Response;
            response.StatusCode = 301;
            response.Headers[HeaderNames.Location] = url;
            context.Result = RuleResult.EndResponse;
        }
    }
}
