using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Boilerplate.Factories;
using Boilerplate.Handlers;
using Boilerplate.Handlers.Exception;

namespace Boilerplate
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            AddApiResponseHandler(config);

            EnableCors(config);

            ReplaceExceptionHandler(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void AddApiResponseHandler(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new ApiResponseHandler());
        }

        private static void EnableCors(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            //config.EnableCors();

            config.SetCorsPolicyProviderFactory(new ApiCorsPolicyFactory());
            config.EnableCors();
        }

        private static void ReplaceExceptionHandler(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionHandler), new ApiExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new ApiExceptionLogger());
        }
    }
}
