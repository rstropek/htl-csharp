using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkWebApi.Models;

namespace EntityFrameworkWebApi.Controllers
{
    [ApiController]
    [Route("api/Persons")]
    public class PersonsController : ControllerBase
    {
        private readonly AddressBookContext context;

        public PersonsController(AddressBookContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<PersonListResult> GetPersons() =>
            // Note how we use `Include` to join Person and Group tables.
            // For details see https://docs.microsoft.com/en-us/ef/core/querying/related-data
            // Exercise: Turn on logging (see AddressBookContext.cs) and review SQL statement(s)
            context.Persons
                .Include(p => p.Group)
                .Select(p => (new PersonListResult
                {
                    PersonID = p.PersonID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    GroupName = p.Group.Name
                }));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            var person = await context.Persons
                .Include(p => p.Group)
                .Select(p => new PersonListResult
                {
                    PersonID = p.PersonID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    GroupName = p.Group.Name
                })
                .SingleOrDefaultAsync(m => m.PersonID == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (id != person.PersonID)
            {
                return BadRequest();
            }

            context.Entry(person).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            context.Persons.Add(person);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.PersonID }, person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            var person = await context.Persons.SingleOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            context.Persons.Remove(person);
            await context.SaveChangesAsync();

            return Ok(person);
        }

        private bool PersonExists(int id)
        {
            return context.Persons.Any(e => e.PersonID == id);
        }
    }
}