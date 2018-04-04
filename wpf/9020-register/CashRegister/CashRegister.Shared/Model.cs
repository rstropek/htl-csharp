using System;
using System.Collections.Generic;

namespace CashRegister.Shared
{
    public class Product
    {
        public int ID { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }
    }

    public class ReceiptLine
    {
        public int ID { get; set; }

        public Product Product { get; set; }

        public int Amount { get; set; }

        public decimal TotalPrice { get; set; }
    }

    public class Receipt
    {
        public int ID { get; set; }

        public DateTime ReceiptTimestamp { get; set; }

        public List<ReceiptLine> ReceiptLines { get; set; }

        public decimal TotalPrice { get; set; }
    }

    public class ReceiptLineDto
    {
        public int ProductID { get; set; }

        public int Amount { get; set; }
    }
}
