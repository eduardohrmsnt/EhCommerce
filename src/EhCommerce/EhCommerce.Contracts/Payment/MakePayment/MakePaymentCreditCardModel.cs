namespace EhCommerce.Contracts.Payment.MakePayment
{
    public class MakePaymentCreditCardModel
    {
        public string? CreditCardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public string? CVV { get; set; }
        public decimal CreditCardAmount { get; set; }
    }
}
