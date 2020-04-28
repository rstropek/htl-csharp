using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.WebApi.Data;

/*
 * NO NEED TO CHANGE SOMETHING IN THIS FILE DURING THE EXERCISE
 */

namespace TaxiManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/taxis")]
    public class TaxisController : ControllerBase
    {
        private readonly TaxiDataContext DbContext;

        public TaxisController(TaxiDataContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Taxi>> GetAllTaxis()
        {
            return await DbContext.Taxis.ToListAsync();
        }
    }
}
