using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace AdvancedEfCore
{
    public class BookDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Note that this time we play with SQLite. You can find a browser
            // at https://sqlitebrowser.org/dl/.
            var dbFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "mydb.db");
            optionsBuilder.UseSqlite($"Data Source={dbFileName};");
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }

        public DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many entity has a multi-part key
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.AuthorID, ba.BookID });

            // Define foreign keys to book and author
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithOne(b => b.Book);
            modelBuilder.Entity<Author>()
                .HasMany(b => b.Books)
                .WithOne(b => b.Author);

            // Note how we create unique indexes
            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.GenreTitle)
                .IsUnique();
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();
        }
    }
}
