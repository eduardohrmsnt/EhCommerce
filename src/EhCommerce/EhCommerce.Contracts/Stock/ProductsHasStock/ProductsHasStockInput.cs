using EhCommerce.Contracts.ShoppingCart.GetShoppingCartById;
using EhCommerce.Shared.Application;

namespace EhCommerce.Contracts.Stock.ProductsHasStock
{
    public class ProductsHasStockInput : IUseCaseRequest<ProductsHasStockOutput>
    {
        public List<ProductCheckStockGenericModel> Products { get; set; }

        public static ProductsHasStockInput FromShoppingCartGenericModel(ShoppingCartGenericModelOutput shoppingCart)
        {
            return new ProductsHasStockInput
            {
                Products = shoppingCart.Products.Select(ProductCheckStockGenericModel.FromShoppingCartProduct).ToList()
            };
        }
    }
}
