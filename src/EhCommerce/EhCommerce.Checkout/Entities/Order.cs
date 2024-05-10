using EhCommerce.Checkout.ValueObjects;
using EhCommerce.Language;
using EhCommerce.Shared.Domain;
using EhCommerce.Shared.Validator;

namespace EhCommerce.Checkout.Entities
{
    public sealed class Order : Entity, IAggregateRoot
    {
        public Order(Guid clientId,
                     Address address,
                     ShippingData shippingData,
                     List<Product> products,
                     Guid paymentId,
                     Coupon? coupon = null)
        {
            ClientId = clientId;
            Address = address;
            ShippingData = shippingData;
            Coupon = coupon;
            PaymentId = paymentId;
            _products = products ?? new List<Product>();
            Validate();
        }

        public Guid ClientId { get; }

        public Guid PaymentId { get; }

        public decimal GrossTotalPrice => _products.Sum(p => p.GrossPrice) + ShippingData.Price;

        public decimal NetTotalPrice => _products.Sum(p => p.NetPrice) + ShippingData.Price;

        public Address Address { get; }

        public ShippingData ShippingData { get; }

        public Coupon? Coupon { get; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        private List<Product> _products;

        protected override void Validate()
        {
            Validator.Contract.ShouldNotBeNull(nameof(Address), Address)
                              .ShouldNotBeEmpty(nameof(ClientId), ClientId)
                              .ShouldNotBeEmpty(nameof(PaymentId), PaymentId)
                              .ShouldNotBeEmpty(nameof(Products), _products)
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
