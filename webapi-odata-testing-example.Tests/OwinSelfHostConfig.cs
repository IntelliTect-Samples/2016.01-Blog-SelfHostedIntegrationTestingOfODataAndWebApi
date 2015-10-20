using System.Web.Http;
using System.Web.OData.Extensions;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace Example.Tests
{
    public class OwinSelfHostConfig
    {
        public static readonly IKernel Kernel = new StandardKernel();

        public void Configuration( IAppBuilder app )
        {
            var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };

            config.MapODataServiceRoute( "TestOdataRoute",
                    null,
                    ModelBuilder.GetEdmModel() );

            config.EnsureInitialized();

            app.UseNinjectMiddleware( CreateKernel ).UseNinjectWebApi( config );
        }

        private static IKernel CreateKernel()
        {
            return Kernel;
        }
    }
}