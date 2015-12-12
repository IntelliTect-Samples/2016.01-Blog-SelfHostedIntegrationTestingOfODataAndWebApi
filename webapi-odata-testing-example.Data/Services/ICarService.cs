using System.Linq;
using System.Threading.Tasks;
using Example.Data.Models;

namespace Example.Data.Services
{
    public interface ICarService
    {
        Task<Car> Create(Car newCar);
        Task<int> Delete(Car delete);
        Task<Car> Find(int id);
        IQueryable<Car> FindAll();
        Task<Car> Update(Car updated);
    }
}