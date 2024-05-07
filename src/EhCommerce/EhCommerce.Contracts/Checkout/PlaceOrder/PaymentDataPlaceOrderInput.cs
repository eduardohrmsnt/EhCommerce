namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PaymentDataPlaceOrderInput
    {
        public string? CreditCardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public string? CVV { get; set; }
        public decimal CreditCardAmount { get; set; }
    }
}
