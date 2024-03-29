using Aibidia.API.Common.Interfaces;
using Aibidia.API.Common.Models;
using Aibidia.API.Common.Utils;
using System.Net.Http;
using Unity;

namespace Aibidia.API.Common.Config
{
    /// <summary>
    /// Common DI registrations
    /// </summary>
    public static class CommonUnityConfig
    {

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterRepositoryTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterSingleton<HttpClient>();
            // Register the Application DBContext
            // container.RegisterType<IDbContextProvider<ApplicationDbContext>, DbContextProvider>();
        }
    }
}