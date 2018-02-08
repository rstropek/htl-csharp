using Microsoft.EntityFrameworkCore;

namespace TemperatureLogger
{
    // Required exercise: Create Entity Framework context
    public class TemperatureContext : DbContext
    {
        public DbSet<TemperatureReading> TemperatureReadings { get; set; }

        // Optional exercise: Store alerts
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Required exercise: Install NuGet package for DB server and use it
            optionsBuilder.UseSqlServer("Server=(localdb)\\dev;Database=TemperatureLogger");
        }
    }
}
