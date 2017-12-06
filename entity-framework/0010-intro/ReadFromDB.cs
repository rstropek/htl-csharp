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
