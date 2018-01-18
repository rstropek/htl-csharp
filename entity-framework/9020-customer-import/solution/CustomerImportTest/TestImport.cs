using CustomerImport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerImportTest
{
    [TestClass]
    public class TestImport
    {
        [TestMethod]
        public async Task Import()
        {
            const string importText = "ID,FirstName,LastName,Birthday,Address.City,Address.Country\n" +
                "1,Claudia,Dorran,1968-04-13,Stari Grad,Croatia\n" +
                "2,Martguerita,Kearle,1988-02-05,,\n" +
                "3,Bradly,Bentham3,,Abeokuta,Nigeria\n" +
                "4,Tim,Smith,,Abeokuta,Nigeria\n" +
                "\n";

            using (var db = (CustomerContext)new CustomerTestContext())
            {
                await db.ImportAsync(importText);

                // Verify number of imported records
                Assert.AreEqual(2, db.Cities.Count());
                Assert.AreEqual(4, db.Customers.Count());

                // Verify imported content
                Assert.AreEqual("Stari Grad", db.Customers.First(c => c.FirstName == "Claudia").City.CityName);
                Assert.AreEqual(new DateTime(1988, 2, 5), db.Customers.First(c => c.FirstName == "Martguerita").Birthday);
            }
        }
    }
}
