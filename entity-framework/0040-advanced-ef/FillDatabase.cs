using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdvancedEfCore
{
    public partial class Program
    {
        static async Task CleanDatabaseAsync(BookDataContext context)
        {
            // Note that we are using a DB transaction here. Either all records are
            // inserted or none of them (A in ACID).
            using var transaction = context.Database.BeginTransaction();

            // Note that we are using a "Raw" SQL statement here. With that, we can use
            // all features of the underlying database. We are not limited to what EFCore
            // can do.
            await context.Database.ExecuteSqlRawAsync("DELETE FROM BookAuthors");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Books");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Genre");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Authors");

            await transaction.CommitAsync();
        }

        private static async Task FillGenreAsync(BookDataContext context)
        {
            var genreData = await File.ReadAllTextAsync("Data/Genre.json");
            var genre = JsonSerializer.Deserialize<IEnumerable<Genre>>(genreData);

            using var transaction = context.Database.BeginTransaction();

            // Note how we combine transaction with exception handling
            try
            {
                // Note that we add all genre data rows BEFORE calling SaveChanges.
                foreach (var g in genre)
                {
                    context.Genre.Add(g);
                }

                await context.SaveChangesAsync();

                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Something bad happened: {ex.Message}");

                // Note re-throw of exception
                throw;
            }
        }

        private static async Task FillBooksAsync(BookDataContext context)
        {
            // Demonstrate tracking queries here. Set a breakpoint up in
            // FillGenreAsync when genre rows are added. Afterwards, show that
            // the query returns THE SAME objects because of identical primary keys.
            var genre = await context.Genre.ToArrayAsync();

            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(
                await File.ReadAllTextAsync("Data/Books.json"));

            using var transaction = context.Database.BeginTransaction();

            var rand = new Random();
            foreach(var book in books)
            {
                var dbBook = new Book
                {
                    Genre = genre[rand.Next(genre.Length)],
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Language = book.Language
                };
                context.Books.Add(dbBook);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        private static async Task FillAuthorsAsync(BookDataContext context)
        {
            // Note that we are jus reading primary keys of books, not entire 
            // book records. Tip: Always read only those columns that you REALLY need.
            var bookIDs = await context.Books.Select(b => b.BookID).ToArrayAsync();

            var authors = JsonSerializer.Deserialize<IEnumerable<Author>>(
                await File.ReadAllTextAsync("Data/Authors.json"));

            using var transaction = context.Database.BeginTransaction();

            var rand = new Random();
            foreach (var author in authors)
            {
                var dbAuthor = new Author
                {
                    AuthorName = author.AuthorName,
                    Nationality = author.Nationality
                };

                // Randomly assign each author one book.
                // Note that we can use the dbAuthor, although we have not yet written
                // it to the database. Also note that we are using the book ID as a
                // foreign key.
                var dbBookAuthor = new BookAuthor
                {
                    Author = dbAuthor,
                    BookID = bookIDs[rand.Next(bookIDs.Length)]
                };

                // Note that we do NOT need to add dbAuthor. It is referenced by
                // dbBookAuthor, that is enough.
                context.BookAuthors.Add(dbBookAuthor);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}
