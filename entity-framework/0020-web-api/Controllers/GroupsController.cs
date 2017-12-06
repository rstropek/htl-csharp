using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkWebApi.Models;

namespace EntityFrameworkWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private readonly AddressBookContext context;

        public GroupsController(AddressBookContext context)
        {
            // Note that ASP.NET will automatically provide a DB context
            // via constructor injection. For details about ASP.NET DI see
            // https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db#register-your-context-with-dependency-injection
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Group> GetGroups() =>
            // Note how we use `Include` to join Group and Person tables.
            // For details see https://docs.microsoft.com/en-us/ef/core/querying/related-data
            context.Groups.Include(g => g.Persons);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup([FromRoute] int id)
        {
            var group = await context.Groups
                .Include(g => g.Persons)
                .SingleOrDefaultAsync(m => m.GroupID == id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpGet("{id}/persons")]
        public IEnumerable<Person> GetGroupMembers([FromRoute] int id) =>
            context.Persons.Where(p => p.GroupID == id);

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup([FromRoute] int id, [FromBody] Group group)
        {
            if (id != group.GroupID)
            {
                return BadRequest();
            }

            // Set status of provided group to modified. This will trigger
            // an update in the database.
            context.Entry(group).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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
        public async Task<IActionResult> PostGroup([FromBody] Group group)
        {
            context.Groups.Add(group);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = group.GroupID }, group);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] int id)
        {
            var group = await context.Groups.SingleOrDefaultAsync(m => m.GroupID == id);
            if (group == null)
            {
                return NotFound();
            }

            context.Groups.Remove(group);
            await context.SaveChangesAsync();

            return Ok(group);
        }

        private bool GroupExists(int id)
        {
            return context.Groups.Any(e => e.GroupID == id);
        }
    }
}