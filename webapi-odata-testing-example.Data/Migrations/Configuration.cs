using System.Data.Entity.Migrations;
using Example.Data.Models;

namespace Example.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<RaceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RaceContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Cars.AddOrUpdate( car => car.Name,
                new Car {  }
                );

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
