using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EntityFrameworkWebApi.Models
{
    public class AddressBookContext : DbContext
    {
        // Create a logger factory for logging to console. For details see
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/logging
        public static readonly LoggerFactory DbLoggerFactory
            = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public AddressBookContext(DbContextOptions<AddressBookContext> options)
            : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // Add logger factory (see above)
            => optionsBuilder.UseLoggerFactory(DbLoggerFactory);
    }
}
