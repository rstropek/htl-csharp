using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvancedEfCore
{
    public class Genre
    {
        public int GenreID { get; set; }

        [Required]
        [MaxLength(50)]
        public string GenreTitle { get; set; }

        public List<Book> Books { get; set; }
    }

    public class Book
    {
        public int BookID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string ISBN { get; set; }

        public int GenreID { get; set; }

        [MaxLength(2)]
        public string Language { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public List<BookAuthor> Authors { get; set; }
    }

    public class Author
    {
        public int AuthorID { get; set; }

        [Required]
        [MaxLength(100)]
        public string AuthorName { get; set; }

        [MaxLength(2)]
        public string Nationality { get; set; }

        public List<BookAuthor> Books { get; set; }
    }

    // Note that we create an entity that represents the many-to-many relationship
    // between books and authors. See BookDataContext.OnModelCreating for code that
    // configures this entity.

    /*
     *  +-------------+         +-------------+         +-------------+
     *  |             |         |             |         |             |
     *  |    Book     + 1 --- m + BookAuthor  + m --- 1 +    Author   |
     *  |             |         |             |         |             |
     *  +-------------+         +-------------+         +-------------+
     */

    public class BookAuthor
    {
        public int BookID { get; set; }

        [Required]
        public Book Book { get; set; }

        public int AuthorID { get; set; }

        [Required]
        public Author Author { get; set; }
    }

    
}
