using Aibidia.API.Common.Constants;
using Aibidia.API.Common.Interfaces;
using Aibidia.API.Common.Repositories;
using Aibidia.API.Common.Models;
using System.Web;

namespace Aibidia.API.Common.Utils
{
    public class DbContextProvider : IDbContextProvider<ApplicationDbContext>
    {
        /// <summary>
        /// Get the current db context FOR THIS REQUEST.
        /// </summary>
        /// <returns></returns>
        public ApplicationDbContext GetCurrentDbContext()
        {
            // "Although the PerRequestLifetimeManager lifetime manager works correctly and can help in
            // working with stateful or thread-unsafe dependencies within the scope of an HTTP request,
            // it is generally not a good idea to use it when it can be avoided, as it can often lead
            // to bad practices or hard to find bugs in the end-user's application code when used
            // incorrectly. It is recommended that the dependencies you register are stateless and if
            // there is a need to share common state between several objects during the lifetime of
            // an HTTP request, then you can have a stateless service that explicitly stores and
            // retrieves this state using the Items collection of the Current object."
            // https://docs.microsoft.com/en-us/previous-versions/msp-n-p/dn200217(v%3Dpandp.30)

            ApplicationDbContext context = (ApplicationDbContext)HttpContext.Current.Items[HttpContextItems.CommonDbContext];
            if (context == null)
            {
                context = CreateNewCurrentDbContext();
                HttpContext.Current.Items[HttpContextItems.CommonDbContext] = context;
            }

            return context;
        }

        /// <summary>
        /// Get a new instance of the current db context. Use with caution. Make sure you don't mix entities from different contexts.
        /// </summary>
        /// <returns></returns>
        public ApplicationDbContext CreateNewCurrentDbContext()
        {
            return DbContextFactory.Create();
        }
    }
}