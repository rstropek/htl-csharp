using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkWebApi.Models
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options)
            : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
