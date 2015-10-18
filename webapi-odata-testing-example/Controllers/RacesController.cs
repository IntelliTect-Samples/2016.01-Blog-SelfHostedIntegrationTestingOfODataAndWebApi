using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Example.Data.Models;
using Example.Data.Services;

namespace Example.Controllers
{
    [ODataRoutePrefix( "Races" )]
    public class RacesController : ODataController
    {
        public RacesController( IRaceService raceService )
        {
            RaceService = raceService;
        }

        private IRaceService RaceService { get; set; }

        [EnableQuery]
        public IQueryable<Race> Get()
        {
            return RaceService.FindAll();
        }

        public async Task<IHttpActionResult> Get( [FromODataUri] int key )
        {
            Race result = await RaceService.Find( key );

            if ( result == null )
            {
                return NotFound();
            }
            return Ok( result );
        }
    }
}