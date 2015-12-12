using System;
using System.Collections.Generic;
using System.Linq;
using Example.Tests.Client.Example;
using Example.Tests.Client.Example.Data.Models;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Example.Tests.Controllers.Drivers
{
    [TestClass]
    public class FullIntegrationTest
    {
        private const string BaseAddress = "http://localhost:19001/";

        [TestMethod]
        public void ItShouldReturnDataFromSql()
        {
            // Arrange
            var container = new ExampleContainer( new Uri( BaseAddress ) );

            using ( WebApp.Start( BaseAddress, TestHelpers.ConfigureWebApi )
                    )
            {
                // Act 
                IEnumerable<Driver> response =
                        container.Drivers.Execute();


                // Assert 
                Assert.AreEqual( 3, response.Count() );
            }
        }
    }
}