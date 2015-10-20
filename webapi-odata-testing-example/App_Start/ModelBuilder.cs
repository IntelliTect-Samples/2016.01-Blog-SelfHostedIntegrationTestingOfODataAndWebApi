using System.Web.OData.Builder;
using Example.Data.Models;
using Microsoft.OData.Edm;

namespace Example
{
    public static class ModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder
            {
                Namespace = "Example",
                ContainerName = "ExampleContainer"
            };
            builder.EntitySet<Race>("Races");
            builder.EntitySet<Driver>("Drivers");
            builder.EntitySet<Car>("Cars");
            builder.EntitySet<RaceResult>("RaceResults");

            return builder.GetEdmModel();
        }
    }
}