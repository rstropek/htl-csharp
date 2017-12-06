using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkWebApi.Models;
using AutoMapper;

namespace EntityFrameworkWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Persons")]
    public class PersonsController : Controller
    {
        private readonly AddressBookContext context;
        private readonly IMapper mapper;

        public PersonsController(AddressBookContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<PersonListResult> GetPersons() =>
            context.Persons
                .Include(p => p.Group)
                .Select(p => mapper.Map<PersonListResult>(new
                {
                    p.PersonID,
                    p.FirstName,
                    p.LastName,
                    GroupName = p.Group.Name
                }));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            var person = await context.Persons
                .Include(p => p.Group)
                .Select(p => new
                {
                    p.PersonID,
                    p.FirstName,
                    p.LastName,
                    GroupName = p.Group.Name
                })
                .SingleOrDefaultAsync(m => m.PersonID == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PersonListResult>(person));
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