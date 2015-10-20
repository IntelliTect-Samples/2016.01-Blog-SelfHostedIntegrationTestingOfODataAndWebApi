using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Extensions;
using Example.Data.Models;
using Example.Data.Services;
using Example.Tests.Client.Example;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Ploeh.AutoFixture;

namespace Example.Tests
{
    [TestClass]
    public class SimpleGetTests
    {
        private const string BaseAddress = "http://localhost:19001/";

        private static Fixture Fixture
        {
            get
            {
                var fixture = new Fixture();
                fixture.Behaviors.Remove( new ThrowingRecursionBehavior() );
                fixture.Behaviors.Add( new OmitOnRecursionBehavior() );
                return fixture;
            }
        }

        [TestMethod]
        public void WhenGettingAllCarsItShouldReturnData()
        {
            // Arrange
            var service = new Mock<ICarService>();
            service.Setup( m => m.FindAll() ).Returns( Fixture.CreateMany<Car>( 10 ).AsQueryable() );
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => ConfigureWebApi( app, kernel ) )
                    )
            {
                // Act 
                IEnumerable<Client.Example.Data.Models.Car> response =
                        container.Cars.Execute();


                // Assert 
                Assert.AreEqual( 10, response.Count() );
                service.Verify( m => m.FindAll(), Times.Once );
            }
        }

        private void ConfigureWebApi( IAppBuilder app, IKernel kernel )
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