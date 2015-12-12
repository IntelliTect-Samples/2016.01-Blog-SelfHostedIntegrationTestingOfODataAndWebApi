using System.Linq;
using System.Threading.Tasks;
using Example.Data.Interfaces;
using Example.Data.Models;

namespace Example.Data.Services
{
    public class RaceService : DataServiceBase, IRaceService
    {
        public IQueryable<Race> FindAll()
        {
            return Db.Races.AsQueryable();
        }

        public async Task<Race> Find( int id )
        {
            return await Db.Races.FindAsync( id );
        }

        public async Task<Race> Create( Race newRace )
        {
            Db.Races.Add( newRace );
            await Db.SaveChangesAsync();
            return newRace;
        }

        public async Task<Race> Update( Race updated )
        {
            if ( updated == null )
            {
                return null;
            }

            Race existing = await Db.Races.FindAsync( updated.Id );
            if ( existing == null )
            {
                return null;
            }

            Db.Entry( existing ).CurrentValues.SetValues( updated );
            await Db.SaveChangesAsync();
            return existing;
        }

        public async Task<int> Delete( Race delete )
        {
            Db.Races.Remove( delete );
            return await Db.SaveChangesAsync();
        }
    }
}