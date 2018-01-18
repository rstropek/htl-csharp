using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerImport
{
    public static class CustomerImporter
    {
        // The following method was not requested in the test. I just added it
        // to make testing a little bit easier.
        public static async Task CleanupTargetDatbaseAsync(this CustomerContext db)
        {
            await db.Database.ExecuteSqlCommandAsync("DELETE FROM dbo.Customers");
            await db.Database.ExecuteSqlCommandAsync("DELETE FROM dbo.Cities");
        }

        public static async Task ImportAsync(this CustomerContext db, string importText)
        {
            // Split text into lines, skip first and last lines, split every line into columns
            var customerSource = importText.Split('\n').Skip(1).Where(row => !string.IsNullOrEmpty(row))
                .Select(row => row.Split(',')).Select(tuple => new
                {
                    ID = Int32.Parse(tuple[0]),
                    FirstName = tuple[1],
                    LastName = tuple[2],
                    Birthday = string.IsNullOrEmpty(tuple[3]) ? (DateTime?)null : DateTime.Parse(tuple[3]),
                    City = tuple[4],
                    Country = tuple[5]
                })
                .ToArray();

            // Extract distinct cities
            var cities = customerSource
                .Where(c => !string.IsNullOrEmpty(c.Country) && !string.IsNullOrEmpty(c.City))
                .Select(c => new { c.City, c.Country })
                .Distinct()
                .Select(c => new City { CityName = c.City, Country = c.Country })
                .ToArray();

            // Create customer objects
            var customers = customerSource
                .Select(c => new Customer
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Birthday = c.Birthday,
                    City = cities.FirstOrDefault(a => a.CityName == c.City && a.Country == c.Country)
                })
                .ToArray();

            // Add customer objects to database and store changes
            await db.Customers.AddRangeAsync(customers);
            await db.SaveChangesAsync();
        }
    }
}
