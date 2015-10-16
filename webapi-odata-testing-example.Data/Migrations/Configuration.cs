using System.Data.Entity.Migrations;
using Example.Data.Models;

namespace Example.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<RaceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed( RaceContext context )
        {
            //  This method will be called after migrating to the latest version.

            context.Cars.AddOrUpdate( car => car.Id,
                    new Car
                    {
                            Id = 1,
                            Name = "007",
                            Make = "Aston Martin",
                            Year = 1964,
                            Model = "DB5",
                            Driver = new Driver { Id = 1, Name = "James Bond" }
                    },
                    new Car
                    {
                            Id = 2,
                            Name = "The Bandit",
                            Make = "Pontiac",
                            Year = 1977,
                            Model = "Trans Am",
                            Driver = new Driver { Id = 2, Name = "Burt Reynolds" }
                    },
                    new Car
                    {
                            Id = 3,
                            Name = "American Grafffiti",
                            Make = "Ford",
                            Model = "Coupe",
                            Year = 1932,
                            Driver = new Driver { Id = 3, Name = "George Lucas" }
                    }
                    );
        }
    }
}