using System;
using System.Linq;
using Example.Data.Interfaces;
using Example.Data.Services;
using Example.Tests.Client.Example;
using Example.Tests.Client.Example.Data.Models;
using Microsoft.OData.Client;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using Ploeh.AutoFixture;

namespace Example.Tests.Controllers.Cars
{
    [TestClass]
    public class WhenGettingCars
    {
        private const string BaseAddress = "http://localhost:19001/";

        [TestMethod]
        public void ItShouldReturnData()
        {
            // Arrange
            var service = new Mock<ICarService>();
            service.Setup( m => m.FindAll() )
                    .Returns( TestHelpers.Fixture.CreateMany<Data.Models.Car>( 10 ).AsQueryable() );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) )
                    )
            {
                // Act 
                var response =
                        container.Cars.Execute();


                // Assert 
                Assert.AreEqual( 10, response.Count() );
                service.Verify( m => m.FindAll(), Times.Once );
            }
        }

        [TestMethod]
        public void ByKeyShouldReturnASingleCar()
        {
            // Arrange
            var service = new Mock<ICarService>();
            service.Setup( m => m.Find( It.IsAny<int>() ) )
                    .ReturnsAsync( TestHelpers.Fixture.Create<Data.Models.Car>() );

            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) )
                    )
            {
                // Act 
                var response = container.Cars.ByKey( 5 ).GetValue();


                // Assert 
                Assert.IsNotNull( response );
                service.Verify( m => m.Find( It.Is<int>( i => i == 5 ) ), Times.Once );
            }
        }

        [TestMethod]
        public void NonExistentCarReturnsNotFound()
        {
            // Arrange
            var service = new Mock<ICarService>();
            service.Setup( m => m.Find( It.IsAny<int>() ) ).ReturnsAsync( null );

            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) )
                    )
            {
                // Act 
                try
                {
                    container.Cars.ByKey( 50 ).GetValue();
                }
                catch ( DataServiceQueryException exception )
                {
                    // Assert 
                    Assert.IsNotNull( exception.InnerException );
                    var inner = (DataServiceClientException) exception.InnerException;
                    Assert.AreEqual( 404, inner.StatusCode );
                    return;
                }

                Assert.Fail( "Exception not caught." );
            }
        }
    }
}