using Microsoft.EntityFrameworkCore;

namespace EFIntro
{
    class AddressBookContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("AddressBook");
        }
    }
}
