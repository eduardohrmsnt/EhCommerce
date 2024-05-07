namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class AddressPlaceOrderInput
    {
        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? BuildingNumber { get; set; }

        public string? Description { get; set; }

        public string? ZipCode { get; set; }
    }
}
