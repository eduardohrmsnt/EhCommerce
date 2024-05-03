using EhCommerce.Checkout.ValueObjects;
using EhCommerce.Language;
using EhCommerce.Shared.Domain;
using EhCommerce.Shared.Extensions;
using EhCommerce.Shared.Validator;

namespace EhCommerce.Checkout.Entities
{
    public sealed class Order : Entity, IAggregateRoot
    {
        public Order(Address address,
                     ShippingData shippingData,
                     List<Payment> payments,
                     Coupon? coupon = null)
        {
            Address = address;
            ShippingData = shippingData;
            Payments = payments;
            Coupon = coupon;
            _products = new List<Product>();
            Validate();
        }

        public decimal GrossTotalPrice => _products.Sum(p => p.GrossPrice) + ShippingData.Price;

        public decimal NetTotalPrice => _products.Sum(p => p.NetPrice) + ShippingData.Price;

        public Address Address { get; }

        public ShippingData ShippingData { get; }

        public List<Payment> Payments { get; }

        public Coupon? Coupon { get; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private List<Product> _products;

        public void AddItem(Product product)
        {
            if (product is null)
                Validator.Contract.ShouldNotBeNull(nameof(Product), product).Validate();

            _products.Add(product);
        }

        protected override void Validate()
        {
            Validator.Contract.ShouldHaveItems(nameof(Payments), Payments)
                              .ShouldBeFalse(nameof(Payments), Payments?.Count > 1 && (Payments?.All(c => !c.GetType().Equals(typeof(CreditCardPayment))) ?? false), DomainMessages.OnlyOneTypeOfPaymentAllowed)
                              .ShouldNotBeNull(nameof(Address), Address)
                              .ShouldNotBeEmpty(nameof(Address.Street), Address?.Street)
                              .ShouldNotBeEmpty(nameof(Address.Country), Address?.Country)
                              .ShouldNotBeEmpty(nameof(Address.State), Address?.State)
                              .ShouldNotBeEmpty(nameof(Address.City), Address?.City)
                              .ShouldNotBeEmpty(nameof(Address.ZipCode), Address?.ZipCode)
                              .ShouldNotBeEmpty(nameof(Address.BuildingNumber), Address?.BuildingNumber)
                              .ShouldNotBeNull(nameof(ShippingData), ShippingData)
                              .ShouldNotBeEmpty(nameof(ShippingData.ShippingCompanyDocument), ShippingData?.ShippingCompanyDocument)
                              .Validate();
        }
    }
}
