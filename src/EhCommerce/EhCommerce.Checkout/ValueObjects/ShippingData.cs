namespace EhCommerce.Checkout.ValueObjects
{
    public class ShippingData
    {
        public ShippingData(decimal amount, 
                            string shippingCompanyDocument)
        {
            ShippingCompanyDocument = shippingCompanyDocument;
            Amount = amount;
        }

        public string ShippingCompanyDocument { get; }
        public decimal Amount { get; }
    }
}
