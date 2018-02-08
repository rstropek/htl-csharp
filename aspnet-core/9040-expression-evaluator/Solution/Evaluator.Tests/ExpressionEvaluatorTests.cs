using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evaluate.Logic;

namespace Evaluate.Tests
{
    [TestClass]
    public class ExpressionEvaluatorTest
    {
        // Required exercise: Show a simple unit test
        [TestMethod]
        public void TestExpression()
        {
            var evaluator = new ExpressionEvaluator();
            Assert.AreEqual(6, evaluator.Evaluate("1+2+3"));
        }

        [TestMethod]
        public void TestExpressionWithVariable()
        {
            var evaluator = new ExpressionEvaluator();
            evaluator.SetVariable("x", 4);
            Assert.AreEqual(10, evaluator.Evaluate("1+2+x+3"));
        }
    }
}
