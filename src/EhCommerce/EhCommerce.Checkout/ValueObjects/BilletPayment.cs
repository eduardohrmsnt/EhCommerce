namespace EhCommerce.Checkout.ValueObjects
{
    public class BilletPayment : Payment
    {
        public BilletPayment(string url) 
        { 
            BilletUrl = url;
        }

        public string BilletUrl { get; }
    }
}
