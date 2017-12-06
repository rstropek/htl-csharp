namespace EntityFrameworkWebApi.Models
{
    public class Person
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GroupID { get; set; }

        public Group Group { get; set; }
    }
}