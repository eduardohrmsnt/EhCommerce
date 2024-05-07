namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class ShippingDataPlaceOrderInput
    {
        public string? ShippingCompanyDocument { get; set; }

        public decimal Price { get; set; }
    }
}
