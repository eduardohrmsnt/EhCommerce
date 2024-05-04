using EhCommerce.Checkout.Application.UseCases.PlaceOrder;
using EhCommerce.Shared.Application;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public interface IPlaceOrderUseCase : IUseCase<PlaceOrderInput, PlaceOrderOutput>
    {
    }
}
