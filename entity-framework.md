# Entity Framework (EF)

O/RM in .NET Core


<!-- .slide: class="left" -->
## What is EF?

* Object-relational mapper (O/RM)
* Supports many different DB providers ([list](https://docs.microsoft.com/en-us/ef/core/providers/index))<br/>
  examples:
  * MS SQL Server
  * SQLite
  * PostgreSQL
  * MySQL
  * In-Memory (for testing)
* Latest version: *EF Core 2.0*
* NuGet (example): [*Microsoft.EntityFrameworkCore.SqlServer*](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)


<!-- .slide: class="left" -->
## Getting Started

* Follow [*installing* docs](https://docs.microsoft.com/en-us/ef/core/get-started/install/index) to add EF to project
* Work through [tutorials](https://docs.microsoft.com/en-us/ef/core/get-started/) in EF docs
* Tips:
  * Install and use [SQL Server 2017 Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQL Server 2017 Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-2016-express-localdb)
  * Use [In-Memory DB](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/) for simple test scenarios


<!-- .slide: class="left" -->
## Building a Model

```
namespace EFIntro
{
    class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
```

* Read more about [creating a Model](https://docs.microsoft.com/en-us/ef/core/modeling/)


<!-- .slide: class="left" -->
## Setting up the Context

```
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
```


<!-- .slide: class="left" -->
## Writing Data

```
using System.Threading.Tasks;

namespace EFIntro
{
    partial class Program
    {
        async static Task WriteToDB(AddressBookContext db) 
        {
            db.Persons.AddRange(new [] {
                new Person() { FirstName = "Tom", LastName = "Turbo" },
                new Person() { FirstName = "Foo", LastName = "Bar" }
            });
            await db.SaveChangesAsync();
        }
    }
}
```

* Read more about [writing data](https://docs.microsoft.com/en-us/ef/core/saving/)


<!-- .slide: class="left" -->
## Querying Data

```
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace EFIntro
{
    partial class Program
    {
        async static Task ReadFromDB(AddressBookContext db) 
        {
            foreach(var person in await db.Persons
                .Where(p => p.LastName.StartsWith("B")).ToArrayAsync())
            {
                Console.WriteLine($"{person.LastName}, {person.FirstName}");
            }
        }
    }
}
```

* Read more about [querying data](https://docs.microsoft.com/en-us/ef/core/querying/)


<!-- .slide: class="left" -->
## Further Readings and Exercises

* Readings
  * [Entity Framework Documentation](https://docs.microsoft.com/en-us/ef/#pivot=efcore)
  * [EF Tutorial with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro)
  