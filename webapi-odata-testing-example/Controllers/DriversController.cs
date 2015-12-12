using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Example.Data.Interfaces;
using Example.Data.Models;

namespace Example.Controllers
{
    public class DriversController : ODataController
    {
        public DriversController( IDriverService driverService )
        {
            DriverService = driverService;
        }

        private IDriverService DriverService { get; }


        [EnableQuery]
        public IQueryable<Driver> Get()
        {
            return DriverService.FindAll();
        }

        public async Task<IHttpActionResult> Get( [FromODataUri] int key )
        {
            var result = await DriverService.Find( key );

            if ( result == null )
            {
                return NotFound();
            }
            return Ok( result );
        }

        public async Task<IHttpActionResult> Post( Driver driver )
        {
            if ( ModelState.IsValid == false )
            {
                return BadRequest( ModelState );
            }
            await DriverService.Create( driver );
            if ( driver.Id == 0 )
            {
                return BadRequest();
            }
            return Created( driver );
        }

        public async Task<IHttpActionResult> Put( [FromODataUri] int key, Driver driver )
        {
            if ( driver == null ||
                 ModelState.IsValid == false )
            {
                return BadRequest();
            }

            var original = await DriverService.Find( key );

            if ( original == null )
            {
                return NotFound();
            }

            await DriverService.Update( driver );

            return Updated( driver );
        }

        public async Task<IHttpActionResult> Delete( [FromODataUri] int key )
        {
            var deletable = await DriverService.Find( key );

            if ( deletable == null )
            {
                return NotFound();
            }

            var recordsDeleted = await DriverService.Delete( deletable );

            if ( recordsDeleted <= 0 )
            {
                return BadRequest();
            }

            return StatusCode( HttpStatusCode.NoContent );
        }
    }
}