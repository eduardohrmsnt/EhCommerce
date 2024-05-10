using EhCommerce.Shared.Domain;

namespace EhCommerce.ShoppingCart.Entities
{
    public class Product : Entity
    {
        public Product(string sku,
                       decimal grossPrice,
                       decimal netPrice)
        {
            Sku = sku;
            GrossPrice = grossPrice;
            NetPrice = netPrice;
        }

        public int Quantity { get; private set; }
        public string Sku { get; }
        public decimal GrossPrice { get; }
        public decimal NetPrice { get; private set; }

        public void AddQuantity(int quantity) => Quantity += quantity;

        public void RemoveQuantity(int quantity) => Quantity -= quantity;

        public void ApplyDiscountByPercentage(decimal percentage)
        {
            NetPrice *= (percentage / 100);
        }

        public void ApplyDiscountByValue(decimal value)
        {
            NetPrice -= value;
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
