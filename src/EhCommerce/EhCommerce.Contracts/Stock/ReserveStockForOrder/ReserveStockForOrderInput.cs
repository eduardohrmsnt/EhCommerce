using EhCommerce.Checkout.Entities;
using EhCommerce.Contracts.Checkout.PlaceOrder;
using EhCommerce.Shared.Application;

namespace EhCommerce.Contracts.Stock.ReserveStockForOrder
{
    public class ReserveStockForOrderInput : IUseCaseRequest<ReserveStockForOrderOutput>
    {
        public Guid OrderId { get; set; }

        public List<ReserveStockForOrderProductModel> ProductsToReserveStock { get; set; }

        public static ReserveStockForOrderInput FromOrder(Order order)
        {
            return new ReserveStockForOrderInput
            {
                OrderId = order.Id,
                ProductsToReserveStock = order.Products.Select(ReserveStockForOrderProductModel.FromProduct).ToList()
            };
        }
    }
}
