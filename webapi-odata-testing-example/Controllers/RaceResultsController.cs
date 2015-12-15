using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using Example.Data.Interfaces;
using Example.Data.Models;

namespace Example.Controllers
{
    public class RaceResultsController : ODataController
    {
        public RaceResultsController( IRaceResultsService resultsService )
        {
            ResultsService = resultsService;
        }

        private IRaceResultsService ResultsService { get; }

        [EnableQuery]
        public IQueryable<RaceResult> Get()
        {
            return ResultsService.FindAll();
        }

        public async Task<IHttpActionResult> Get( [FromODataUri] int key )
        {
            RaceResult result = await ResultsService.Find( key );

            if ( result == null )
            {
                return NotFound();
            }
            return Ok( result );
        }

        public async Task<IHttpActionResult> Post( RaceResult raceResult )
        {
            if ( ModelState.IsValid == false )
            {
                return BadRequest( ModelState );
            }
            await ResultsService.Create( raceResult );
            if ( raceResult.Id == 0 )
            {
                return BadRequest();
            }
            return Created( raceResult );
        }

        public async Task<IHttpActionResult> Put( [FromODataUri] int key, RaceResult raceResult )
        {
            if ( raceResult == null ||
                 ModelState.IsValid == false )
            {
                return BadRequest();
            }

            RaceResult original = await ResultsService.Find( key );

            if ( original == null )
            {
                return NotFound();
            }

            await ResultsService.Update( raceResult );

            return Updated( raceResult );
        }

        public async Task<IHttpActionResult> Delete( [FromODataUri] int key )
        {
            RaceResult deletable = await ResultsService.Find( key );

            if ( deletable == null )
            {
                return NotFound();
            }

            int recordsDeleted = await ResultsService.Delete( deletable );

            if ( recordsDeleted <= 0 )
            {
                return BadRequest();
            }

            return StatusCode( HttpStatusCode.NoContent );
        }
    }
}