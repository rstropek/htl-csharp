using Prism.Mvvm;

namespace CashRegister.UICore
{
    public class ReceiptLineViewModel : BindableBase
    {
        private int productID;
        public int ProductID
        {
            get { return productID; }
            set { SetProperty(ref productID, value); }
        }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { SetProperty(ref productName, value); }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { SetProperty(ref totalPrice, value); }
        }
    }
}
