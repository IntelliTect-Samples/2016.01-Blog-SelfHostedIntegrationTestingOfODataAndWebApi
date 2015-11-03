using System.Linq;
using System.Threading.Tasks;
using Example.Data.Interfaces;
using Example.Data.Models;

namespace Example.Data.Services
{
    public class CarService : DataServiceBase, ICarService
    {
        public IQueryable<Car> FindAll()
        {
            return Db.Cars.AsQueryable();
        }

        public async Task<Car> Find( int id )
        {
            return await Db.Cars.FindAsync( id );
        }

        public async Task<Car> Create( Car newCar )
        {
            Db.Cars.Add( newCar );
            await Db.SaveChangesAsync();
            return newCar;
        }

        public async Task<Car> Update( Car updated )
        {
            if ( updated == null )
            {
                return null;
            }

            Car existing = await Db.Cars.FindAsync( updated.Id );
            if ( existing == null )
            {
                return null;
            }

            Db.Entry( existing ).CurrentValues.SetValues( updated );
            await Db.SaveChangesAsync();
            return existing;
        }

        public async Task<int> Delete( Car delete )
        {
            Db.Cars.Remove( delete );
            return await Db.SaveChangesAsync();
        }
    }
}