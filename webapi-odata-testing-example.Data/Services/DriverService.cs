using System.Linq;
using System.Threading.Tasks;
using Example.Data.Interfaces;
using Example.Data.Models;

namespace Example.Data.Services
{
    public class DriverService : DataServiceBase, IDriverService
    {
        public IQueryable<Driver> FindAll()
        {
            return Db.Drivers.AsQueryable();
        }

        public async Task<Driver> Find( int id )
        {
            return await Db.Drivers.FindAsync( id );
        }

        public async Task<Driver> Create( Driver newDriver )
        {
            Db.Drivers.Add( newDriver );
            await Db.SaveChangesAsync();
            return newDriver;
        }

        public async Task<Driver> Update( Driver updated )
        {
            if ( updated == null )
            {
                return null;
            }

            Driver existing = await Db.Drivers.FindAsync( updated.Id );
            if ( existing == null )
            {
                return null;
            }

            Db.Entry( existing ).CurrentValues.SetValues( updated );
            await Db.SaveChangesAsync();
            return existing;
        }

        public async Task<int> Delete( Driver delete )
        {
            Db.Drivers.Remove( delete );
            return await Db.SaveChangesAsync();
        }
    }
}