using EhCommerce.Shared.Domain;

namespace EhCommerce.Checkout.ValueObjects
{
    public class ShippingData : IValueObject
    {
        public ShippingData(decimal amount, 
                            string? shippingCompanyDocument)
        {
            ShippingCompanyDocument = shippingCompanyDocument;
            Price = amount;
        }

        public string? ShippingCompanyDocument { get; }
        public decimal Price { get; }
    }
}
