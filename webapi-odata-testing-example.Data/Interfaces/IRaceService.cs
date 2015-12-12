using System.Linq;
using System.Threading.Tasks;
using Example.Data.Models;

namespace Example.Data.Interfaces
{
    public interface IRaceService
    {
        IQueryable<Race> FindAll();
        Task<Race> Find(int id);
        Task<Race> Create(Race newRace);
        Task<Race> Update(Race updated);
        Task<int> Delete(Race delete);
        void Dispose();
    }
}