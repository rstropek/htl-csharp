using TemperatureLogger;
using Microsoft.EntityFrameworkCore;

namespace TemperatureLoggerTest
{
    public class TemperatureTestContext : TemperatureContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Optional exercise: Use InMemory DB for testing
            optionsBuilder.UseInMemoryDatabase("TemperatureLogger");
        }
    }
}
