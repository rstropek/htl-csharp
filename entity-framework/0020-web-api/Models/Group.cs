using System.Collections.Generic;

namespace EntityFrameworkWebApi.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public List<Person> Persons { get; set; }
    }
}
