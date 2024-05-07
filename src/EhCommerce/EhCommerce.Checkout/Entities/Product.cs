using EhCommerce.Shared.Domain;
using EhCommerce.Shared.Validator;

namespace EhCommerce.Checkout.Entities
{
    public sealed class Product : Entity
    {
        public Product(decimal grossPrice,
                       decimal netPrice,
                       string sku,
                       int quantity)
        {
            GrossPrice = grossPrice;
            NetPrice = netPrice;
            Sku = sku;
            Quantity = quantity;
            Validate();

        }

        public decimal GrossPrice { get; private set; }

        public decimal NetPrice { get; private set; }

        public string Sku { get; private set; }

        public int Quantity { get; private set; }

        protected override void Validate()
        {
            Validator.Contract.ShouldBeGreaterThan(nameof(GrossPrice), GrossPrice, 0)
                .ShouldBeGreaterThan(nameof(NetPrice), NetPrice, 0)
                .ShouldBeLessThan(nameof(NetPrice), NetPrice, GrossPrice)
                .ShouldBeGreaterThan(nameof(Quantity), Quantity, 0)
                .ShouldNotBeEmpty(nameof(Sku), Sku).Validate();
        }
    }
}
