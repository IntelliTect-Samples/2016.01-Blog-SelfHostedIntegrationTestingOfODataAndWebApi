using System.Data.Entity;
using Example.Data.Models;

namespace Example.Data
{
    public class RaceContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Race> Races { get; set; } 

        public DbSet<RaceResult> RaceResults { get; set; }
    }
}