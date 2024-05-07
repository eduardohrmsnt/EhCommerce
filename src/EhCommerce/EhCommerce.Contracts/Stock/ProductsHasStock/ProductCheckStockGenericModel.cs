using EhCommerce.Contracts.ShoppingCart.GetShoppingCartById;

namespace EhCommerce.Contracts.Stock.ProductsHasStock
{
    public class ProductCheckStockGenericModel
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public static ProductCheckStockGenericModel FromShoppingCartProduct(ShoppingCartProductModelOutput shoppingCartProduct)
        {
            return new ProductCheckStockGenericModel
            {
                ProductId = shoppingCartProduct.ProductId,
                Quantity = shoppingCartProduct.Quantity,
            };
        }
    }
}
