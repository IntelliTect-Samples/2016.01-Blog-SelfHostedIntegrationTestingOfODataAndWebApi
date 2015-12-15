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
    public class WhenPostingCars
    {
        private const string BaseAddress = "http://localhost:19001/";

        [TestMethod]
        public void ItSavesNewCar()
        {
            // Arrange
            var newCar = new Car
                         {
                                 Name = "Hakosuka",
                                 Make = "Nissan",
                                 Model = "Skyline GTR",
                                 Year = 1996
                         };

            var service = new Mock<ICarService>();
            service.Setup( m => m.Create( It.IsAny<Data.Models.Car>() ) )
                    .ReturnsAsync( TestHelpers.Fixture.Create<Data.Models.Car>() );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                // Act 
                container.AddToCars( newCar );
                ChangeOperationResponse response = container.SaveChanges().Cast<ChangeOperationResponse>().First();
                var entityDescriptor = (EntityDescriptor) response.Descriptor;
                var actual =
                        (Car) entityDescriptor.Entity;

                // Assert 
                Assert.AreEqual( (int) HttpStatusCode.Created, response.StatusCode );
                service.Verify( m => m.Create( It.IsAny<Data.Models.Car>() ), Times.Once );
                Assert.IsNotNull( actual );
            }
        }


        [TestMethod]
        public void IfMissingRequiredFieldsItReturnsBadRequest()
        {
            // Arrange
            var badCar = new Car
                         {
                                 Name = "Empty"
                         };

            var service = new Mock<ICarService>();
            service.Setup( m => m.Create( It.IsAny<Data.Models.Car>() ) )
                    .ReturnsAsync( TestHelpers.Fixture.Create<Data.Models.Car>() );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                // Act
                try
                {
                    container.AddToCars( badCar );
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
        public void IfNewCarHasZeroIdItReturnsBadRequest()
        {
            // Arrange
            var errorCar = TestHelpers.Fixture.Create<Data.Models.Car>();
            errorCar.Id = 0;
            var service = new Mock<ICarService>();
            service.Setup( m => m.Create( It.IsAny<Data.Models.Car>() ) )
                    .ReturnsAsync( errorCar );

            // Bind our mock with Ninject
            var kernel = new StandardKernel();
            kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, app => TestHelpers.ConfigureWebApi( app, kernel ) ) )
            {
                // Act
                try
                {
                    container.AddToCars( new Car
                                         {
                                                 Name = "Hakosuka",
                                                 Make = "Nissan",
                                                 Model = "Skyline GTR",
                                                 Year = 1996
                                         } );
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
    }
}