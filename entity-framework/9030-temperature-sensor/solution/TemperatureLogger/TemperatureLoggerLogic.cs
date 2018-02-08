using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace TemperatureLogger
{
    public static class TemperatureLoggerLogic
    {
        // The following method was not requested in the test. I just added it
        // to make testing a little bit easier.
        public static async Task CleanupTargetDatbaseAsync(this TemperatureContext db)
        {
            await db.Database.ExecuteSqlCommandAsync("DELETE FROM dbo.TemperatureReadings");
            await db.Database.ExecuteSqlCommandAsync("DELETE FROM dbo.Alerts");
        }

        public static async Task StoreTemperatureAsync(this TemperatureContext db, double temperature)
        {
            var reading = new TemperatureReading
            {
                MeasureDateTime = DateTime.UtcNow,
                Temperature = temperature
            };

            // Required exercise: Add record to database
            await db.TemperatureReadings.AddAsync(reading);

            // Optional exercise: Add alert to database
            var message = temperature < -10d ? "Too low" : temperature > 25d ? "Too high" : null;
            if (message != null)
            {
                await db.Alerts.AddAsync(new Alert { Message = message, TemperatureReading = reading });
            }

            await db.SaveChangesAsync();
        }
    }
}
