using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Aibidia.API.Common.Attributes
{
    public class GlobalCacheControlAttribute : ActionFilterAttribute
    {
        /// <summary>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null && actionExecutedContext.Response.Headers.CacheControl == null)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    NoStore = true
                };
            }
        }
    }
}
