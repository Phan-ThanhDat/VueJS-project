using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Aibidia.API.Common.Attributes
{
    public class ContentDispositionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            if (context.Response?.Content != null && context.Response.Content.Headers.ContentDisposition == null)
            {
                context.Response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "api.json"
                };
            }
        }        
    }
}
