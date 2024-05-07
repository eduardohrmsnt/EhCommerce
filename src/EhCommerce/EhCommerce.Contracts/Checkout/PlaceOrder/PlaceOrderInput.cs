using EhCommerce.Shared.Application;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PlaceOrderInput : IUseCaseRequest<PlaceOrderOutput>
    {
        public Guid ShoppingCartId { get; set; }

        public AddressPlaceOrderInput? Address { get; set; }

        public ShippingDataPlaceOrderInput? ShippingData { get; set; }

        public PaymentPlaceOrderInput? Payment { get; set; }
    }
}
