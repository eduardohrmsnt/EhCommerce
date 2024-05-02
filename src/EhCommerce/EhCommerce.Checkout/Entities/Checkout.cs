using EhCommerce.Checkout.ValueObjects;
using EhCommerce.Language;
using EhCommerce.Shared.Domain;
using EhCommerce.Shared.Extensions;

namespace EhCommerce.Checkout.Entities
{
    public class Checkout : IAggregateRoot
    {
        public Checkout(Address address,
                        ShippingData shippingData,
                        List<Payment> payments,
                        Coupon coupon)
        {
            Address = address;
            ShippingData = shippingData;
            Payments = payments;
            Coupon = coupon;
            Validate();
        }

        public Address Address { get; }
        public ShippingData ShippingData { get; }
        public List<Payment> Payments { get; }
        public Coupon Coupon { get; }

        private void Validate()
        {
            if (Payments is null || Payments.Count == 0)
                throw new DomainException(DomainMessages.ObrigatoryField.FormatWith(nameof(Payments)));

            if (Payments.Count > 1 && Payments.All(c => !c.GetType().Equals(typeof(CreditCardPayment))))
                throw new DomainException(DomainMessages.OnlyOneTypeOfPaymentAllowed.FormatWith(nameof(Payments)));
        }
    }
}
