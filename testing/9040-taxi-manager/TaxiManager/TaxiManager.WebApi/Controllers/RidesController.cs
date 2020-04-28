using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.WebApi.Data;

namespace TaxiManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/rides")]
    public class RidesController : ControllerBase
    {
        /// <summary>
        /// Start a new ongoing ride using HTTP POST
        /// </summary>
        /// <param name="newRide">Data about the new taxi ride that should be created</param>
        /// <returns>
        /// Returns "Bad Request" if <paramref name="newRide"/> is null.
        /// Returns "Not Found" if there is no taxi with the ID specified in <paramref name="newRide"/>.
        /// Returns "Not Found" if there is no driver with the ID specified in <paramref name="newRide"/>.
        /// Returns "Created" if the new ride was created successfully. In that case, the newly created
        /// taxi ride object has to be returned in the HTTP response body.
        /// </returns>
        /// <remarks>
        /// Uses <see cref="TaxiDataContext.StartRideAsync"/> to create the new taxi ride.
        /// </remarks>
        [HttpPost]
        public Task<IActionResult> Post([FromBody]StartRideDto newRide)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ends an ongoing ride using HTTP POST
        /// </summary>
        /// <param name="id">ID of the ride to end</param>
        /// <param name="charge">Charge for the ride</param>
        /// <returns>
        /// Returns "Bad Request" if <paramref name="charge"/> is null.
        /// Returns "Not Found" if there is no ride with the ID specified in the URL.
        /// Returns "OK" if the new ride was ended successfully. In that case, the ended
        /// taxi ride object has to be returned in the HTTP response body.
        /// </returns>
        /// <remarks>
        /// Uses <see cref="TaxiDataContext.EndRideAsync(int, decimal)"/> to end the taxi ride.
        /// </remarks>
        [HttpPost]
        [Route("{id}")]
        public Task<IActionResult> EndRide(int id, [FromBody]EndRideDto charge)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all ongoing rides (i.e. rides where <see cref="TaxiRide.End"/> is null)
        /// </summary>
        /// <returns>
        /// Returns "OK" with a collection of result objects in the HTTP response body.
        /// </returns>
        [HttpGet]
        [Route("ongoing")]
        public Task<IEnumerable<TaxiRide>> GetAllOngoingRides()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all completed rides (i.e. rides where <see cref="TaxiRide.End"/> is not null)
        /// </summary>
        /// <returns>
        /// Returns "OK" with a collection of result objects in the HTTP response body.
        /// </returns>
        [HttpGet]
        [Route("completed")]
        public Task<IEnumerable<TaxiRide>> GetAllCompletedRides()
        {
            throw new NotImplementedException();
        }
    }
}
