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
    public class WhenPuttingCars
    {
        private const string BaseAddress = "http://localhost:19002/";

        [TestMethod]
        public void IsUpdatesTheCar()
        {
            // Arrange
            var storedCar = TestHelpers.Fixture.Create<Data.Models.Car>();
            var service = new Mock<ICarService>();
            service.Setup( m => m.Find( It.IsAny<int>() ) ).ReturnsAsync( storedCar );
            service.Setup( m => m.Update( It.IsAny<Data.Models.Car>() ) ).ReturnsAsync( storedCar );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                // Act
                Car target = container.Cars.ByKey( storedCar.Id ).GetValue();
                target.Name = TestHelpers.Fixture.Create<string>();
                container.UpdateObject( target );
                ChangeOperationResponse response =
                        container.SaveChanges( SaveChangesOptions.ReplaceOnUpdate )
                                .Cast<ChangeOperationResponse>()
                                .First();
                var entityDescriptor = (EntityDescriptor) response.Descriptor;
                var actual = (Car) entityDescriptor.Entity;

                // Assert
                Assert.AreEqual( (int) HttpStatusCode.NoContent, response.StatusCode );
                Assert.IsNotNull( actual );
                Assert.AreEqual( storedCar.Make, actual.Make );
                Assert.AreEqual( storedCar.Model, actual.Model );
                Assert.AreEqual( storedCar.Year, actual.Year );
            }
        }

        [TestMethod]
        public void IfInvalidCarItReturnsNotFound()
        {
            // Arrange
            var service = new Mock<ICarService>();
            service.Setup( m => m.FindAll() )
                    .Returns( TestHelpers.Fixture.CreateMany<Data.Models.Car>().AsQueryable() );
            service.Setup( m => m.Find( It.IsAny<int>() ) ).ReturnsAsync( null );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );
            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                try
                {
                    // Act
                    Car first = container.Cars.Execute().First();
                    first.Make = "FOO";
                    container.UpdateObject( first );
                    container.SaveChanges( SaveChangesOptions.ReplaceOnUpdate );
                }
                catch ( DataServiceRequestException exception )
                {
                    var inner = exception.InnerException as DataServiceClientException;
                    Assert.IsNotNull( inner );
                    Assert.AreEqual( (int) HttpStatusCode.NotFound, inner.StatusCode );
                    service.Verify( x => x.Update( It.IsAny<Data.Models.Car>() ), Times.Never );
                    return;
                }
            }

            Assert.Fail( "Exception not caught." );
        }
    }
}