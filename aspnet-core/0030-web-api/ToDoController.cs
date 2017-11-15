using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace app
{
    [Route("api/todo-items")]
    public partial class ToDoController : Controller
    {
        private static List<string> items = new List<String> { "Clean my room", "Feed the cat" };

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(items);
        }
    }
}
