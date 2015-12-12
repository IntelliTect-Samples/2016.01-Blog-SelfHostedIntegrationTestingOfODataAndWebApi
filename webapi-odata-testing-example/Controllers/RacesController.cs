using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Example.Data.Interfaces;
using Example.Data.Models;

namespace Example.Controllers
{
    public class RacesController : ODataController
    {
        public RacesController( IRaceService raceService )
        {
            RaceService = raceService;
        }

        private IRaceService RaceService { get; }

        [EnableQuery]
        public IQueryable<Race> Get()
        {
            return RaceService.FindAll();
        }

        public async Task<IHttpActionResult> Get( [FromODataUri] int key )
        {
            var result = await RaceService.Find( key );

            if ( result == null )
            {
                return NotFound();
            }
            return Ok( result );
        }

        public async Task<IHttpActionResult> Post( Race race )
        {
            if ( ModelState.IsValid == false )
            {
                return BadRequest( ModelState );
            }
            await RaceService.Create( race );
            if ( race.Id == 0 )
            {
                return BadRequest();
            }
            return Created( race );
        }

        public async Task<IHttpActionResult> Put( [FromODataUri] int key, Race race )
        {
            if ( race == null ||
                 ModelState.IsValid == false )
            {
                return BadRequest();
            }

            var original = await RaceService.Find( key );

            if ( original == null )
            {
                return NotFound();
            }

            await RaceService.Update( race );

            return Updated( race );
        }

        public async Task<IHttpActionResult> Delete( [FromODataUri] int key )
        {
            var deletable = await RaceService.Find( key );

            if ( deletable == null )
            {
                return NotFound();
            }

            var recordsDeleted = await RaceService.Delete( deletable );

            if ( recordsDeleted <= 0 )
            {
                return BadRequest();
            }

            return StatusCode( HttpStatusCode.NoContent );
        }
    }
}