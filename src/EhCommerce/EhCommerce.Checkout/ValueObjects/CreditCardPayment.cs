namespace EhCommerce.Checkout.ValueObjects
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string creditCardNumber,
                                 string expirationDate,
                                 string cardHolderName)
        {
            CreditCardNumber = creditCardNumber;
            ExpirationDate = expirationDate;
            CardHolderName = cardHolderName;
        }

        public string CreditCardNumber { get; }
        public string ExpirationDate { get; }
        public string CardHolderName { get; }
    }
}
