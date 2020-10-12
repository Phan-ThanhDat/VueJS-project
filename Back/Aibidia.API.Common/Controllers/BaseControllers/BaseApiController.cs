using System.Web.Http;
using Aibidia.API.Common.Models;
using Aibidia.API.Common.Interfaces;
using Aibidia.API.Common.Utils;

namespace Aibidia.API.Common.Controllers.BaseControllers
{
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// Get the context for the current tenant or the context of the requested tenant.
        /// </summary>
        protected ApplicationDbContext Context
        {
            get
            {
                if (_context == null)
                {
                    // TODO: Get current db context somehow
                }
                return _context;
            }
        }
        private ApplicationDbContext _context;
        private IDbContextProvider<ApplicationDbContext> _contextProvider = new DbContextProvider(); // TODO: Dependency injection

        /// <summary>
        /// Get the current tenant context FOR THIS REQUEST.
        /// </summary>
        /// <returns></returns>
        protected ApplicationDbContext GetCurrentDbContext() => _contextProvider.GetCurrentDbContext();

        /// <summary>
        /// Get a new instance of the current tenant context. Use with caution. Make sure you don't mix entities from different contexts.
        /// </summary>
        /// <returns></returns>
        protected ApplicationDbContext CreateNewCurrentDbContext() => _contextProvider.CreateNewCurrentDbContext();

        protected IHttpActionResult NotFound(string message)
        {
            return Content(System.Net.HttpStatusCode.NotFound, new HttpError(message));
        }
        protected IHttpActionResult NotFound(string parameterName, object parameterValue)
        {
            return Content(System.Net.HttpStatusCode.NotFound, new HttpError($"{parameterName} (id: '{parameterValue}') not found."));
        }
        protected IHttpActionResult Conflict(string message)
        {
            return Content(System.Net.HttpStatusCode.Conflict, new HttpError(message));
        }

        /// <summary>
        /// Dispose current tenant context for this request.
        /// https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application#close-database-connections
        /// </summary>
        /// <returns></returns>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}