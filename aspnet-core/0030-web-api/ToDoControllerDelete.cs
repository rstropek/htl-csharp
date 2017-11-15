using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
                return NoContent();
            }

            return BadRequest("Invalid index");
        }
    }
}
