using Microsoft.AspNetCore.Mvc;
using Evaluate.Logic;
using Evaluator.Models;

namespace Evaluate.Controllers
{
    [Route("api/evaluate")]
    public class EvaluateController : Controller
    {
        private IExpressionEvaluator evaluator;

        // Optional exercise: Use ASP.NET Core Dependency Injection
        public EvaluateController(IExpressionEvaluator evaluator)
        {
            this.evaluator = evaluator;
        }

        // Required exercise: Demonstrate creation of a Web API POST method
        [HttpPost]
        public IActionResult Post([FromBody]EvalExpression expression) => Ok(new ExpressionWithResult
        {
            Expression = expression.Expression,
            Result = evaluator.Evaluate(expression.Expression)
        });
    }
}
