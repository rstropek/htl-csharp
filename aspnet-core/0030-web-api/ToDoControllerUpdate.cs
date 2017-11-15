using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpPut]
        [Route("{index}")]
        public IActionResult UpdateItem(int index, [FromBody] string newItem)
        {
            if (index >= 0 && index < items.Count)
            {
                items[index] = newItem;
                return Ok();
            }

            return BadRequest("Invalid index");
        }
    }
}
