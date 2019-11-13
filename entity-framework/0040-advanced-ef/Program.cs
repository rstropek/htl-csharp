using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdvancedEfCore
{
    // Note the use of a pratial class.

    public static partial class Program
    {
        static async Task Main()
        {
            using var context = new BookDataContext();
            await CleanDatabaseAsync(context);
            await FillGenreAsync(context);
            await FillBooksAsync(context);
            await FillAuthorsAsync(context);

            var books = await QueryBooksWithAuthorsAsync(context);
            books = await QueryFilteredBooksAsync(context);
            books = await context.Books.EnglishBooks().BooksWithTitleFilter().ToArrayAsync();
        }
    }
}
