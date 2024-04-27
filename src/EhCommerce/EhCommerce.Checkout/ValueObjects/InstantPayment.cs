namespace EhCommerce.Checkout.ValueObjects
{
    public class InstantPayment : Payment
    {
        public InstantPayment(string randomKey)
        {
            RandomKey = randomKey;
        }

        public string RandomKey { get; private set; }
    }
}
