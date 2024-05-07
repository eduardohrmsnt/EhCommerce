namespace EhCommerce.Contracts.ShoppingCart.GetShoppingCartById
{
    public class CouponModelOutput
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public int Percentage { get; set; }
    }
}
