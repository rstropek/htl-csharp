using Microsoft.AspNetCore.Mvc;
using Evaluate.Logic;
using Evaluator.Models;
using System.Linq;

namespace Evaluate.Controllers
{
    // Optional exercise: Support variables

    [Route("api/variables")]
    public class VariablesController : Controller
    {
        private IExpressionEvaluator evaluator;

        // Optional exercise: Use ASP.NET Core Dependency Injection
        public VariablesController(IExpressionEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        // Optional exercise: Demonstrate creation of a Web API GET method
        [HttpGet]
        public IActionResult Get() => Ok(evaluator.Variables
            // Note: Just returning the Dictionary is fine, too
            .Select(v => new Variable
            {
                Name = v.Key,
                Value = v.Value
            }));

        [HttpPost]
        public IActionResult Post([FromBody] Variable variable)
        {
            evaluator.SetVariable(variable.Name, variable.Value);
            return Ok(variable);
        }
    }
}
