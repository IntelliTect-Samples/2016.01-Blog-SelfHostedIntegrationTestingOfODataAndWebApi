using System.Web.Http;
using System.Web.OData.Extensions;
using Example;
using Microsoft.Owin;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly : OwinStartup( typeof (Startup) )]

namespace Example
{
    public class Startup
    {
        // ReSharper disable once UnusedMember.Global
        public void Configuration( IAppBuilder app )
        {
            var config = new HttpConfiguration();
            config.MapODataServiceRoute( "OdataRoute",
                    null,
                    ModelBuilder.GetEdmModel() );


            config.EnsureInitialized();

            app.UseNinjectMiddleware( CreateKernel ).UseNinjectWebApi( config );
        }


        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind(
                    x =>
                            x.FromAssembliesMatching( "*.Data.dll" )
                                    .SelectAllClasses()
                                    .BindAllInterfaces()
                                    .Configure( binding => binding.InRequestScope() ) );
            return kernel;
        }
    }
}