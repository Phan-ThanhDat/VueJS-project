using System;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web.Http.Filters;

namespace Aibidia.API.Common.Attributes
{
    public class CacheControlAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// How many time units to keep response in cache. Default is 5.
        /// </summary>
        public int TTL { get; set; } = 5;

        /// <summary>
        /// Units developer is able to pass to this Attribute
        /// </summary>
        public TimeUnit Unit { get; set; } = TimeUnit.Minutes;
        
        /// <summary>
        /// Time units of the MaxAge TTL. Default is Minutes.
        /// </summary>
        public enum TimeUnit {
            Seconds = 0,
            Minutes = 1,
            Hours = 2,
            Days = 3
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                TimeSpan maxAge;
                switch (Unit) {
                    case TimeUnit.Seconds:
                        maxAge = TimeSpan.FromSeconds(TTL);
                        break;
                    case TimeUnit.Hours:
                        maxAge = TimeSpan.FromHours(TTL);
                        break;
                    case TimeUnit.Days:
                        maxAge = TimeSpan.FromDays(TTL);
                        break;
                    case TimeUnit.Minutes:
                    default:
                        maxAge = TimeSpan.FromMinutes(TTL);
                        break;
                }
                actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
                {
                    MaxAge = maxAge,
                    MustRevalidate = false,
                    NoStore = true
                };
            }
        }
    }
}
