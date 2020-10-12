using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Converters;
using Aibidia.API.Common.Attributes;
using Aibidia.API.Common.Handlers;
using Aibidia.API.Common.Repositories;
using Homework.Constants;
using Unity;
using Unity.AspNet.WebApi;
using Aibidia.API.Common.Interfaces;

namespace Homework
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable CORS globally
            string corshost = ConfigurationManager.AppSettings["JsHostCors"].ToString();

            // traceparent = ApplicationInsights W3C correlation id header
            var corsAttribute = new EnableCorsAttribute(corshost, "Authorization, Content-Type, Aibidia-Partner-DatabaseId, traceparent, tracestate, Request-Id, Request-Context", "*");
            config.EnableCors(corsAttribute);

            // Convert NotImplementedExceptions to HTTP 501-Not implemented
            config.Filters.Add(new NotImplExceptionFilterAttribute());

            // Serialize enums to strings (they are already deserialized from string to enum)
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            // Trim leading and trailing whitespaces from string properties at the point of serialization
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new TrimmingConverter());

            // Only serialize non-null values
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Add Content-Disposition headers to API responses
            config.Filters.Add(new ContentDispositionAttribute());

            // API version support
            //config.AddApiVersioning();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Add global CacheControl attribute (no-cache)
            config.Filters.Add(new GlobalCacheControlAttribute());

            config.Routes.MapHttpRoute(
                name: RouteConfigs.ApiRouteName,
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "VersionedUrl",
            //    routeTemplate: "api/v{apiVersion}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    constraints: new { apiVersion = new ApiVersionRouteConstraint() }
            //);

            // Enable Unity dependency injection
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.Container);
        }
    }
}
