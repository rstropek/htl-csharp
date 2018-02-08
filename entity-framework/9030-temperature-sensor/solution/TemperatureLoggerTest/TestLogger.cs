using TemperatureLogger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureLoggerTest
{
    [TestClass]
    public class TestLogger
    {
        // Required exercise: Create a unit test
        [TestMethod]
        public async Task Import()
        {
            using (var db = (TemperatureContext)new TemperatureTestContext())
            {
                await db.StoreTemperatureAsync(-30d);
                await db.StoreTemperatureAsync(30d);
                await db.StoreTemperatureAsync(0d);

                Assert.AreEqual(3, db.TemperatureReadings.Count());
                Assert.AreEqual(2, db.Alerts.Count());

                Assert.AreEqual(1, db.Alerts.Count(a => a.Message == "Too low"));
                Assert.AreEqual(1, db.Alerts.Count(a => a.Message == "Too high"));
            }
        }
    }
}
