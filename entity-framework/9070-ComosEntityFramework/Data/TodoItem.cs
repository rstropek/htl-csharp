using System;

namespace ComosEntityFramework.Data
{
    public class TodoItem
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string Description { get; set; }
    }
}
