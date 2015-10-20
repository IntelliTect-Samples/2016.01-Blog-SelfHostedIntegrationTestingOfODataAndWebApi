using System;
using System.Collections.Generic;
using System.Linq;
using Example.Data.Models;
using Example.Data.Services;
using Example.Tests.Client.Example;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            OwinSelfHostConfig.Kernel.Bind<ICarService>().ToConstant( service.Object );
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start<OwinSelfHostConfig>( BaseAddress ) )
            {
                // Act 
                IEnumerable<Client.Example.Data.Models.Car> response =
                        container.Cars.Execute();


                // Assert 
                Assert.AreEqual( 10, response.Count() );
                service.Verify( m => m.FindAll(), Times.Once );
            }
        }
    }
}