using System.Web.Http;
using System.Web.OData.Extensions;
using Example;
using Example.Data.Services;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly : OwinStartup( typeof (Startup) )]

namespace Example
{
    public class Startup
    {
        public void Configuration( IAppBuilder app )
        {
            var config = new HttpConfiguration();
            config.MapODataServiceRoute( "OdataRoute",
                    null,
                    ModelBuilder.GetEdmModel() );


            config.EnsureInitialized();

            app.UseNinjectMiddleware( CreateKernel ).UseNinjectWebApi( config );
        }


        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IRaceService>().To<RaceService>();
            kernel.Bind<IDriverService>().To<DriverService>();
            kernel.Bind<ICarService>().To<CarService>();
            kernel.Bind<IRaceResultsService>().To<RaceResultsService>();
            return kernel;
        }
    }
}