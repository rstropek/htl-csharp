using MvvmDemo;
using System.Linq;
using Xunit;

namespace Turmrechnen.Logic.Tests
{
    public class CalculateTests
    {
        [Fact]
        public void EndResultEqualToStartValue()
        {
            // Prepare
            var logic = new TurmrechnenLogic();

            // Execute
            const int baseValue = 2;
            var result = logic.Calculate(baseValue, 5);

            // Assertation
            Assert.Equal(baseValue, result.First().SourceValue);
            Assert.Equal(baseValue, result.Last().Result);
        }
    }
}
