using CashRegister.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CashRegister.Tests
{
    [TestClass]
    public class TestModel
    {
        [TestMethod]
        public void TestTotalPriceInclusiveVat()
        {
            var receipt = new Receipt
            {
                TotalPrice = 100m,
                VatPercentage = 0.2m
            };

            Assert.AreEqual(120, receipt.TotalPriceInclusiveVat);
        }
    }
}
