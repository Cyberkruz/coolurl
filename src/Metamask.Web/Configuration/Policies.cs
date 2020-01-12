using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace Metamask.Web.Configuration
{
    /// <summary>
    /// Retry and Circuit Breaker pattern policies to harden 
    /// external connections.
    /// </summary>
    public class Policies
    {
        public readonly AsyncRetryPolicy<HttpResponseMessage> RequestTimeoutRetryPolicy;
        public readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> RequestTimeoutCircuitBreaker;

        public Policies()
        {
            RequestTimeoutRetryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, ra => TimeSpan.FromSeconds(Math.Pow(0.2, ra)));

            RequestTimeoutCircuitBreaker = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Builds a list of common codes that are safe
        /// to retry with SQL Client.
        /// </summary>
        /// <returns>A list of the SQL Server common codes.</returns>
        public int[] GetSqlRetryCodes()
        {
            return new int[]
            {
                4060, // login failed
                40197, // processing request error
                40501, // service busy
                40613, // database unavailable
                49918, // not enough resources
                49919, // too many operations
                49920, // too many operations for subscription
                4221 // failed with long wait
            };
        }
    }
}
