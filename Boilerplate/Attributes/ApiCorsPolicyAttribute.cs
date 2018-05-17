using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace Boilerplate.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private readonly CorsPolicy _policy;

        public ApiCorsPolicyAttribute()
        {
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            _policy.Origins.Add("http://localhost:4000");
            _policy.Origins.Add("http://localhost:5000");
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}