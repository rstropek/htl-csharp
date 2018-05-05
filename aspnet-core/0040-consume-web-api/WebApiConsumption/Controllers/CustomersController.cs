using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System;

namespace Consume_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            // Simulate async data access
            await Task.Delay(1);

            // Return result
            return Ok(new[] { new Customer { ID = 1, Name = "Dummy" } });
        }
    }
}
