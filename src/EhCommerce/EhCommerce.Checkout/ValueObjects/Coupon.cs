namespace EhCommerce.Checkout.ValueObjects
{
    public class Coupon
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
