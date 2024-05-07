using EhCommerce.Enums;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PaymentPlaceOrderInput
    {
        public List<PaymentDataPlaceOrderInput>? Data { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
