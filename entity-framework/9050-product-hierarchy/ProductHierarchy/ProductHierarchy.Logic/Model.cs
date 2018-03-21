using System.Collections.Generic;

namespace ProductHierarchy.Logic
{
    // Model classes - their structure reflects the DB structure

    public class Product
    {
        public int ID { get; set; }

        public string ProductNumber { get; set; }

        public string Manufacturer { get; set; }

        public decimal? UnitPrice { get; set; }

        public ICollection<Rebate> Rebates { get; set; }

        public ICollection<ProductHierarchy> ParentProducts { get; set; }

        public ICollection<ProductHierarchy> ChildProducts { get; set; }
    }

    public class Rebate
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public Product Product { get; set; }

        public int MinQuantity { get; set; }

        public decimal RebatePerc { get; set; }
    }

    public class ProductHierarchy
    {
        public int ID { get; set; }

        public int ParentProductID { get; set; }

        public Product ParentProduct { get; set; }

        public int ChildProductID { get; set; }

        public Product ChildProduct { get; set; }

        public int Amount { get; set; }
    }
}
