using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Example.Data.Models;
using Example.Data.Services;

namespace Example.Controllers
{
    public class CarsController : ODataController
    {
        public CarsController( ICarService carService )
        {
            CarService = carService;
        }

        private ICarService CarService { get; set; }

        [EnableQuery]
        public IQueryable<Car> Get()
        {
            return CarService.FindAll();
        }

        public async Task<IHttpActionResult> Get( [FromODataUri] int key )
        {
            Car result = await CarService.Find( key );

            if ( result == null )
            {
                return NotFound();
            }
            return Ok( result );
        }

        public async Task<IHttpActionResult> Post( Car car )
        {
            if ( ModelState.IsValid == false )
            {
                return BadRequest( ModelState );
            }
            Car inserted = await CarService.Create( car );
            if ( inserted.Id == 0 )
            {
                return BadRequest();
            }
            return Created( inserted );
        }

        public async Task<IHttpActionResult> Put( [FromODataUri] int key, Car car )
        {
            if ( car == null ||
                 ModelState.IsValid == false )
            {
                return BadRequest();
            }

            Car original = await CarService.Find( key );

            if ( original == null )
            {
                return NotFound();
            }

            await CarService.Update( car );

            return Updated( car );
        }

        public async Task<IHttpActionResult> Delete( [FromODataUri] int key )
        {
            Car deletable = await CarService.Find( key );

            if ( deletable == null )
            {
                return NotFound();
            }

            int recordsDeleted = await CarService.Delete( deletable );

            if ( recordsDeleted <= 0 )
            {
                return BadRequest();
            }

            return StatusCode( HttpStatusCode.NoContent );
        }
    }
}