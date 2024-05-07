using EhCommerce.Checkout.Entities;

namespace EhCommerce.Checkout.Interfaces
{
    public interface IOrderRepository
    {
        Task<Guid> Insert(Order order, CancellationToken cancellationToken);
    }
}
