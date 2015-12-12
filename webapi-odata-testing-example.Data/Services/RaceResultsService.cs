using System.Linq;
using System.Threading.Tasks;
using Example.Data.Interfaces;
using Example.Data.Models;

namespace Example.Data.Services
{
    public class RaceResultsService : DataServiceBase, IRaceResultsService
    {
        public IQueryable<RaceResult> FindAll()
        {
            return Db.RaceResults.AsQueryable();
        }

        public async Task<RaceResult> Find( int id )
        {
            return await Db.RaceResults.FindAsync( id );
        }

        public async Task<RaceResult> Create( RaceResult newRaceResult )
        {
            Db.RaceResults.Add( newRaceResult );
            await Db.SaveChangesAsync();
            return newRaceResult;
        }

        public async Task<RaceResult> Update( RaceResult updated )
        {
            if ( updated == null )
            {
                return null;
            }

            RaceResult existing = await Db.RaceResults.FindAsync( updated.Id );
            if ( existing == null )
            {
                return null;
            }

            Db.Entry( existing ).CurrentValues.SetValues( updated );
            await Db.SaveChangesAsync();
            return existing;
        }

        public async Task<int> Delete( RaceResult delete )
        {
            Db.RaceResults.Remove( delete );
            return await Db.SaveChangesAsync();
        }
    }
}