using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Aibidia.API.Common.Attributes
{
    /// <summary>
    /// Note that the response may be stored by any cache, even if the response is normally non - cacheable.
    /// For example Cloud Edge caches may also store these results, not just Browser cache.
    /// </summary>
    public class CacheControlRarelyChangingAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {

                    // Max age is 30 days. Please don't change the world boundaries more often.
                    MaxAge = TimeSpan.FromDays(30),
                    // Indicates that once a resource becomes stale, caches must not use their stale copy without successful validation on the origin server.
                    MustRevalidate = false,
                    // The response may be stored by any cache, even if the response is normally non - cacheable.
                    // For example Cloud Edge caches may also store these results, not just Browser cache.
                    Public = true,
                    // Explicitly set NoStore = false, which is default. The response may not be stored in any cache.
                    NoStore = false
                };
            }
        }
    }
}
