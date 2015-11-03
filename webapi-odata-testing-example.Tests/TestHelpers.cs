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