using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.WebApi.Data;

namespace TaxiManager.WebApi.Controllers
{
    [Route("api/rides")]
    public class RidesController : Controller
    {
        private TaxiDataContext DbContext;

        public RidesController(TaxiDataContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StartRideDto newRide)
        {
            if (newRide == null)
            {
                return BadRequest("Missing body");
            }

            var taxi = await DbContext.Taxis.FirstOrDefaultAsync(t => t.ID == newRide.TaxiID);
            if (taxi == null)
            {
                return NotFound("Taxi with specified ID not found");
            }

            var driver = await DbContext.Drivers.FirstOrDefaultAsync(t => t.ID == newRide.DriverID);
            if (driver == null)
            {
                return NotFound("Driver with specified ID not found");
            }

            var newRideID = await DbContext.StartRideAsync(taxi, driver);
            return StatusCode(201, await DbContext.Rides.FirstAsync(r => r.ID == newRideID));
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> EndRide(int id, [FromBody]EndRideDto charge)
        {
            if (charge == null)
            {
                return BadRequest("Missing body");
            }

            var ride = await DbContext.Rides.FirstOrDefaultAsync(t => t.ID == id);
            if (ride == null)
            {
                return NotFound("Ride with specified ID not found");
            }

            await DbContext.EndRideAsync(id, charge.Charge);

            ride = await DbContext.Rides.FirstOrDefaultAsync(t => t.ID == id);
            return Ok(ride);
        }

        private IQueryable<TaxiRide> QueryRides() => DbContext.Rides.Include("Taxi").Include("Driver");

        [HttpGet]
        [Route("ongoing")]
        public async Task<IEnumerable<TaxiRide>> GetAllOngoingRides()
        {
            return await QueryRides().Where(r => !r.End.HasValue).ToListAsync();
        }

        [HttpGet]
        [Route("completed")]
        public async Task<IEnumerable<TaxiRide>> GetAllCompletedRides()
        {
            return await QueryRides().Where(r => r.End.HasValue).ToListAsync();
        }
    }
}
