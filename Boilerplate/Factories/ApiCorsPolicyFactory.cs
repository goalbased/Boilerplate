using System.Net.Http;
using System.Web.Http.Cors;
using Boilerplate.Attributes;

namespace Boilerplate.Factories
{
    public class ApiCorsPolicyFactory : ICorsPolicyProviderFactory
    {
        private readonly ICorsPolicyProvider _provider = new ApiCorsPolicyAttribute();

        public ICorsPolicyProvider GetCorsPolicyProvider(HttpRequestMessage request)
        {
            return _provider;
        }
    }
}