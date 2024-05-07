using EhCommerce.Contracts.Checkout.PlaceOrder;
using EhCommerce.Shared.Application;

namespace EhCommerce.Contracts.ShoppingCart.GetShoppingCartById
{
    public class GetShoppingCartByIdInput : IUseCaseRequest<ShoppingCartGenericModelOutput>
    {
        public Guid ShoppingCartId { get; set; }

        public static GetShoppingCartByIdInput FromPlaceOrderInput(PlaceOrderInput request)
        {
            return new GetShoppingCartByIdInput
            {
                ShoppingCartId = request.ShoppingCartId,
            };
        }
    }
}
