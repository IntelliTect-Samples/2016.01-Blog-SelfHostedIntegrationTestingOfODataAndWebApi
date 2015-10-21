using System.Web.Http;
using System.Web.OData.Extensions;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace Example.Tests
{
    static internal class TestHelpers
    {
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