using System;
using System.Linq;
using System.Net;
using Example.Data.Interfaces;
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
    public class WhenDeletingCars
    {
        private const string BaseAddress = "http://localhost:19001/";


        [TestMethod]
        public void ItDeletesTheCar()
        {
            // Arrange
            var storedCar = TestHelpers.Fixture.Create<Data.Models.Car>();
            var service = new Mock<ICarService>();
            service.Setup( m => m.Find( It.IsAny<int>() ) ).ReturnsAsync( storedCar );
            service.Setup( m => m.Delete( It.IsAny<Data.Models.Car>() ) ).ReturnsAsync( 1 ).Verifiable();

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                // Act
                var target = container.Cars.ByKey( storedCar.Id ).GetValue();
                container.DeleteObject( target );
                var response =
                        container.SaveChanges()
                                .Cast<ChangeOperationResponse>()
                                .First();

                // Assert
                Assert.AreEqual( (int) HttpStatusCode.NoContent, response.StatusCode );
                Assert.IsNull( response.Error );
                service.Verify( m => m.Delete( It.Is<Data.Models.Car>( x => x.Id == storedCar.Id ) ), Times.Once );
            }
        }

        [TestMethod]
        public void ItReturnsBadRequestIfDeleteFails()
        {
            // Arrange
            var storedCar = TestHelpers.Fixture.Create<Data.Models.Car>();
            var service = new Mock<ICarService>();
            service.Setup( m => m.Find( It.IsAny<int>() ) ).ReturnsAsync( storedCar );
            service.Setup( m => m.Delete( It.IsAny<Data.Models.Car>() ) ).ReturnsAsync( 0 );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                // Act
                var target = container.Cars.ByKey( storedCar.Id ).GetValue();
                container.DeleteObject( target );
                try
                {
                    container.SaveChanges();
                }
                catch ( DataServiceRequestException exception )
                {
                    var inner = exception.InnerException as DataServiceClientException;
                    Assert.IsNotNull( inner );
                    Assert.AreEqual( (int) HttpStatusCode.BadRequest, inner.StatusCode );
                    return;
                }

                // Assert
                Assert.Fail( "Exception not caught." );
            }
        }

        [TestMethod]
        public void ItReturnsNotFoundIfCarIsntFound()
        {
            // Arrange
            var service = new Mock<ICarService>();
            service.Setup( m => m.FindAll() ).Returns( TestHelpers.Fixture.CreateMany<Data.Models.Car>().AsQueryable() );
            service.Setup( m => m.Find( It.IsAny<int>() ) ).ReturnsAsync( null );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                // Act
                var target = container.Cars.First();
                container.DeleteObject( target );
                try
                {
                    container.SaveChanges();
                }
                catch ( DataServiceRequestException exception )
                {
                    var inner = exception.InnerException as DataServiceClientException;
                    Assert.IsNotNull( inner );
                    Assert.AreEqual( (int) HttpStatusCode.NotFound, inner.StatusCode );
                    return;
                }

                // Assert
                Assert.Fail( "Exception not caught." );
            }
        }
    }
}