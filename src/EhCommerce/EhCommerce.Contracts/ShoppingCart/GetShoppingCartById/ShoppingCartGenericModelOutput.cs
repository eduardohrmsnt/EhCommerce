namespace EhCommerce.Contracts.ShoppingCart.GetShoppingCartById
{
    public class ShoppingCartGenericModelOutput
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public List<ShoppingCartProductModelOutput> Products { get; set; }

        public CouponModelOutput Coupon { get; set; }
    }
}
