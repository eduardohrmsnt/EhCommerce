using EhCommerce.Enums;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PlaceOrderOutput
    {
        public Guid OrderId { get; set; }

        public PaymentDataPlaceOrderOutput PaymentData { get; set; }
    }
}
