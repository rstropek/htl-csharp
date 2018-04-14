using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.WebApi.Data;

/*
 * NO NEED TO CHANGE SOMETHING IN THIS FILE DURING THE EXAM
 */

namespace TaxiManager.WebApi.Controllers
{
    [Route("api/drivers")]
    public class DriversController : Controller
    {
        private TaxiDataContext DbContext;

        public DriversController(TaxiDataContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
            return await DbContext.Drivers.ToListAsync();
        }
    }
}
