using EhCommerce.Contracts.Checkout.PlaceOrder;

namespace EhCommerce.Checkout.Application.UseCases.PlaceOrder
{
    public class PlaceOrderUseCase : IPlaceOrderUseCase
    {
        public async Task Handle(PlaceOrderInput request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
