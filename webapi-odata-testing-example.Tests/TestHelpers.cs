using System.Web.Http;
using System.Web.OData.Extensions;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Ploeh.AutoFixture;

namespace Example.Tests
{
    internal static class TestHelpers
    {
        internal static Fixture Fixture
        {
            get
            {
                var fixture = new Fixture();
                // Car->Owner->Cars recurses infinitely, so don't make specimens that do that.
                fixture.Behaviors.Remove( new ThrowingRecursionBehavior() );
                fixture.Behaviors.Add( new OmitOnRecursionBehavior() );
                return fixture;
            }
        }


        /// <summary>
        ///     Configures the app with the Ninject kernel with the default IoC bindings.
        /// </summary>
        /// <param name="app">An <see cref="IAppBuilder" /> reference.</param>
        internal static void ConfigureWebApi( IAppBuilder app )
        {
            ConfigureWebApi( app, Startup.CreateKernel() );
        }

        /// <summary>
        ///     Configures the app with a custom Ninject kernel.
        /// </summary>
        /// <param name="app">An <see cref="IAppBuilder" /> reference.</param>
        /// <param name="kernel">The custom Ninject <see cref="IKernel" />.</param>
        internal static void ConfigureWebApi( IAppBuilder app, IKernel kernel )
        {
            var config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };

            config.MapODataServiceRoute( "TestOdataRoute",
                    null,
                    ModelBuilder.GetEdmModel() );

            config.EnsureInitialized();
            app.UseNinjectMiddleware( () => kernel );
            app.UseNinjectWebApi( config );
        }
    }
}