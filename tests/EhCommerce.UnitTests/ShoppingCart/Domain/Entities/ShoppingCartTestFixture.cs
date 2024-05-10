using Entity = EhCommerce.ShoppingCart.Entities;
using EhCommerce.UnitTests.Common;
using Xunit;
using EhCommerce.ShoppingCart.Entities;

namespace EhCommerce.UnitTests.ShoppingCart.Domain.Entities
{
    [CollectionDefinition(nameof(ShoppingCartTestFixture))]
    public class ShoppingCartTestFixtureCollection : ICollectionFixture<ShoppingCartTestFixture> { }
    public class ShoppingCartTestFixture : BaseCatalogFixture
    {
        public ShoppingCartTestFixture()
        {

        }

        public Guid ValidClientId => Faker.Random.Guid();

        public Entity.ShoppingCart ValidShoppingCart => new(ValidClientId);

        public Entity.Product ValidProduct(int quantity)
        {
            var grossPrice = ValidProductPrice;

            var product = new Product(ValidProductSku,
                                      grossPrice,
                                      ValidProductNetPrice(grossPrice));

            product.AddQuantity(quantity);

            return product;
        }
    }
}
