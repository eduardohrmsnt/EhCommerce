namespace EhCommerce.Contracts.ShoppingCart.GetShoppingCartById
{
    public class ShoppingCartProductModelOutput
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal GrossPrice { get; set; }

        public decimal NetPrice { get; set; }

        public string Sku { get; set; }
    }
}
