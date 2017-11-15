using Microsoft.AspNetCore.Mvc;

namespace app
{
    public partial class ToDoController
    {
        [HttpPost]
        public IActionResult AddItem([FromBody] string newItem)
        {
            items.Add(newItem);
            return CreatedAtRoute("GetSpecificItem", new { index = items.IndexOf(newItem) }, newItem);
        }
    }
}
