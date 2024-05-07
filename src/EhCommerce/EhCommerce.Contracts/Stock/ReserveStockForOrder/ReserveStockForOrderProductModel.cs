using EhCommerce.Checkout.Entities;

namespace EhCommerce.Contracts.Stock.ReserveStockForOrder
{
    public class ReserveStockForOrderProductModel
    {
        public Guid ProductId { get; set; }
        public int QuantityToReserve { get; set; }

        public static ReserveStockForOrderProductModel FromProduct(Product product)
        {
            return new ReserveStockForOrderProductModel
            {
                ProductId = product.Id,
                QuantityToReserve = product.Quantity
            };
        }
    }
}
