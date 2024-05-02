using EhCommerce.Shared.Domain;

namespace EhCommerce.Checkout.ValueObjects
{
    public class Coupon : IValueObject
    {
        public Coupon(string code,
                      int percentage,
                      decimal amount)
        {
            Code = code;
            Percentage = percentage;
            Amount = amount;
        }

        public string Code { get; }
        public decimal Amount { get; }
        public int Percentage { get; }
    }
}
