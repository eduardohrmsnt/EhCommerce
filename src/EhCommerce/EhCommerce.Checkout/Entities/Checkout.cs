using EhCommerce.Checkout.ValueObjects;
namespace EhCommerce.Checkout.Entities
{
    public class Checkout
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
        }

        public Address Address { get; }
        public ShippingData ShippingData { get; }
        public List<Payment> Payments { get; }
        public Coupon Coupon { get; }
    }
}
