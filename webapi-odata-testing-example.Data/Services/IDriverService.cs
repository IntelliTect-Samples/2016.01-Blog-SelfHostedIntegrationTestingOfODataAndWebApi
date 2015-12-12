using System.Linq;
using System.Threading.Tasks;
using Example.Data.Models;

namespace Example.Data.Services
{
    public interface IDriverService
    {
        IQueryable<Driver> FindAll();
        Task<Driver> Find( int id );
        Task<Driver> Create( Driver newDriver );
        Task<Driver> Update( Driver updated );
        Task<int> Delete( Driver delete );
        void Dispose();
    }
}