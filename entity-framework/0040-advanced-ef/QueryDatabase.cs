using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AdvancedEfCore
{
    public static partial class Program
    {
        static async Task<IEnumerable<Book>> QueryBooksWithAuthorsAsync(BookDataContext context)
        {
            // Note Include method to query books and genre with a single query.
            return await context
                .Books
                .Include(b => b.Genre)
                .ToArrayAsync();
        }

        static async Task<IEnumerable<Book>> QueryFilteredBooksAsync(BookDataContext context)
        {
            // Note that the query in the next line will NOT be executed immediately.
            // EF Core will run a single query once you call ToArrayAsync. This principle is
            // called Deferred Execution.
            var germanBooks = context
                .Books
                .Where(b => b.Language == "DE" || b.Language == "EN");

            // Both Where clauses are combined with logical AND.
            return await germanBooks
                .Where(b => b.Genre.GenreTitle.Contains("Drama"))
                .ToArrayAsync();
        }

        static IQueryable<Book> EnglishBooks(this IQueryable<Book> books)
        {
            return books.Where(b => b.Language == "US");
        }

        static IQueryable<Book> BooksWithTitleFilter(this IQueryable<Book> books)
        {
            return books.Where(b => b.Title.Contains("of"));
        }
    }
}
