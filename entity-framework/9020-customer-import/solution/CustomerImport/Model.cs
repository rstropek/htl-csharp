using System;
using System.Collections.Generic;

namespace CustomerImport
{
    public class City
    {
        public int ID { get; set; }

        public string CityName { get; set; }

        public string Country { get; set; }
    }

    public class Customer
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public City City { get; set; }
    }
}
