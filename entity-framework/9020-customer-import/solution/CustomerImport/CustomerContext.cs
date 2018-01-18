using Microsoft.EntityFrameworkCore;

namespace CustomerImport
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\dev;Database=Customers");
        }
    }
}
