using EhCommerce.Shared.Domain;
using FluentAssertions;
using Xunit;
using Entity = EhCommerce.ShoppingCart.Entities;

namespace EhCommerce.UnitTests.ShoppingCart.Domain.Entities
{
    [Collection(nameof(ShoppingCartTestFixture))]
    public class ShoppingCartTest
    {
        private readonly ShoppingCartTestFixture _fixture;

        public ShoppingCartTest(ShoppingCartTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(CreateShoppingCartSuccess))]
        [Trait("Domain", "ShoppingCart")]
        public void CreateShoppingCartSuccess()
        {
            var clientId = _fixture.ValidClientId;

            var shoppingCart = new Entity.ShoppingCart(clientId);

            shoppingCart.ClientId.Should().Be(clientId);
        }

        [Fact(DisplayName = nameof(CreateShoppingCartWithEmptyClientIdShouldThrow))]
        [Trait("Domain", "ShoppingCart")]
        public void CreateShoppingCartWithEmptyClientIdShouldThrow()
        {
            var clientId = Guid.Empty;

            var creationOfShoppingCart = () => new Entity.ShoppingCart(clientId);

            creationOfShoppingCart.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "ClientId should not be empty.");
        }

        [Fact(DisplayName = nameof(CreateShoppingCartWithEmptyClientIdShouldThrow))]
        [Trait("Domain", "ShoppingCart")]
        public void AddItemShouldAddProductToListAndUpdateTotals()
        {
            var shoppingCart = _fixture.ValidShoppingCart;
            var validProduct = _fixture.ValidProduct(quantity: 1);

            shoppingCart.AddProduct(validProduct);

            shoppingCart.TotalQuantity.Should().Be(1);
            shoppingCart.TotalGrossPrice.Should().Be(validProduct.GrossPrice);
            shoppingCart.TotalNetPrice.Should().Be(validProduct.NetPrice);
        }

    }
}
