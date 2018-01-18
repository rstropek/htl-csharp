using CustomerImport;
using Microsoft.EntityFrameworkCore;

namespace CustomerImportTest
{
    public class CustomerTestContext : CustomerContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Customers");
        }
    }
}
