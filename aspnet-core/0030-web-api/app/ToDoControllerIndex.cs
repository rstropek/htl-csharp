using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpGet]
        [Route("{index}", Name = "GetSpecificItem")]
        public IActionResult GetItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                return Ok(items[index]);
            }

            return BadRequest("Invalid index");
        }
    }
}
