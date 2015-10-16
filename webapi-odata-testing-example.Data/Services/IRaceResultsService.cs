using System.Linq;
using System.Threading.Tasks;
using Example.Data.Models;

namespace Example.Data.Services
{
    public interface IRaceResultsService
    {
        IQueryable<RaceResult> FindAll();
        Task<RaceResult> Find( int id );
        Task<RaceResult> Create( RaceResult newRaceResult );
        Task<RaceResult> Update( RaceResult updated );
        Task<int> Delete( RaceResult delete );
        void Dispose();
    }
}