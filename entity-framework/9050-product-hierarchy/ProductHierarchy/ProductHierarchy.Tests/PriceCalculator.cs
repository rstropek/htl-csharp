using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductHierarchy.Logic;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ProductHierarchy.Tests
{
    [TestClass]
    public class PriceCalculator
    {
        [TestMethod]
        public async Task ReferenceCalculation()
        {
            using (var context = new ProductHierarchyContext())
            {
                var ppc = new ProductPriceCalculator();
                var costs = await ppc.CalculateProductPriceAsync(context);
                Assert.AreEqual(40240.31m, costs);
            }
        }
    }
}
