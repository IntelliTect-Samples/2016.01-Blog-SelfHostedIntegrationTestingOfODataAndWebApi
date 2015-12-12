using System;
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
                        Owner = new Driver { Id = 1, Name = "James Bond" }
                    },
                    new Car
                    {
                        Id = 2,
                        Name = "The Bandit",
                        Make = "Pontiac",
                        Year = 1977,
                        Model = "Trans Am",
                        Owner = new Driver { Id = 2, Name = "Burt Reynolds" }
                    },
                    new Car
                    {
                        Id = 3,
                        Name = "American Grafffiti",
                        Make = "Ford",
                        Model = "Coupe",
                        Year = 1932,
                        Owner = new Driver { Id = 3, Name = "George Lucas" }
                    }
                    );

            context.Races.AddOrUpdate( race => race.Id,
                    new Race
                    {
                        Id = 1,
                        Name = "The Goldbrick 50",
                        Laps = 50,
                        StartDateTime = new DateTime( 2016, 1, 15, 11, 00, 00, DateTimeKind.Local )
                    },
                    new Race
                    {
                        Id = 2,
                        Name = "The Mudders Milk 200",
                        Laps = 200,
                        StartDateTime = new DateTime( 2016, 5, 1, 14, 30, 00, DateTimeKind.Local )
                    }
                    );

            context.RaceResults.AddOrUpdate( rr => rr.Id,
                    new RaceResult
                    {
                        Id = 1,
                        Car = context.Cars.Find( 1 ),
                        Driver = context.Drivers.Find( 1 ),
                        DidFinish = true,
                        DidStart = true,
                        FinishTimeSpan = new TimeSpan( 0, 0, 34, 27, 134 ),
                        Race = context.Races.Find( 1 ),
                        LapsCompleted = 50
                    },
                    new RaceResult
                    {
                        Id = 2,
                        Car = context.Cars.Find( 2 ),
                        Driver = context.Drivers.Find( 2 ),
                        DidFinish = false,
                        DidStart = true,
                        FinishTimeSpan = null,
                        Race = context.Races.Find( 1 ),
                        LapsCompleted = 10
                    },
                    new RaceResult
                    {
                        Id = 3,
                        Car = context.Cars.Find( 3 ),
                        Driver = context.Drivers.Find( 3 ),
                        DidFinish = true,
                        DidStart = true,
                        FinishTimeSpan = new TimeSpan( 0, 0, 34, 28, 321 ),
                        Race = context.Races.Find( 1 ),
                        LapsCompleted = 49
                    },
                    new RaceResult
                    {
                        Id = 4,
                        Car = context.Cars.Find( 1 ),
                        Driver = context.Drivers.Find( 1 ),
                        DidFinish = true,
                        DidStart = true,
                        FinishTimeSpan = new TimeSpan( 0, 1, 11, 11, 11 ),
                        Race = context.Races.Find( 2 ),
                        LapsCompleted = 200,
                        PitStops = 3
                    },
                    new RaceResult
                    {
                        Id = 5,
                        Car = context.Cars.Find( 2 ),
                        Driver = context.Drivers.Find( 3 ),
                        DidFinish = false,
                        DidStart = false,
                        FinishTimeSpan = null,
                        Race = context.Races.Find( 2 ),
                        LapsCompleted = null,
                        PitStops = null
                    },
                    new RaceResult
                    {
                        Id = 6,
                        Car = context.Cars.Find( 3 ),
                        Driver = context.Drivers.Find( 2 ),
                        DidFinish = true,
                        DidStart = true,
                        FinishTimeSpan = new TimeSpan( 0, 1, 11, 33, 33 ),
                        Race = context.Races.Find( 2 ),
                        LapsCompleted = 200,
                        PitStops = 4
                    }
                    );
        }
    }
}