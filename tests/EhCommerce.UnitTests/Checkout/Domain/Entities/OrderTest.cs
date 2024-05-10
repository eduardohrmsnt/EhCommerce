using EhCommerce.Checkout.Entities;
using EhCommerce.Checkout.ValueObjects;
using EhCommerce.Shared.Domain;
using EhCommerce.Shared.Extensions;
using EhCommerce.UnitTests.Checkout.Domain.Entities;
using FluentAssertions;
using Xunit;
using Entity = EhCommerce.Checkout.Entities;

namespace EhCommerce.UnitTests.Order.Domain.Entities
{
    [Collection(nameof(OrderTestFixture))]
    public class OrderTest
    {
        private readonly OrderTestFixture _orderTestFixture;

        public OrderTest(OrderTestFixture orderTestFixture)
        {
            _orderTestFixture = orderTestFixture;
        }

        [Fact(DisplayName = nameof(CreateOrderSuccess))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderSuccess()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var validCoupon = _orderTestFixture.ValidCoupon;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = new Entity.Order(Guid.NewGuid(),
                                        address,
                                        shippingData,
                                        validProducts,
                                        Guid.NewGuid(),
                                        validCoupon);

            order.Should().NotBeNull();
            order.PaymentId.Should().NotBeEmpty();
            order.Address.Street.Should().Be(address.Street);
            order.Address.State.Should().Be(address.State);
            order.Address.City.Should().Be(address.City);
            order.Address.Country.Should().Be(address.Country);
            order.Address.BuildingNumber.Should().Be(address.BuildingNumber);
            order.Address.ZipCode.Should().Be(address.ZipCode);
            order.Address.Description.Should().Be(address.Description);
            order.ShippingData.ShippingCompanyDocument.Should().Be(shippingData.ShippingCompanyDocument);
            order.ShippingData.Price.Should().Be(shippingData.Price);
            order.Coupon?.Code.Should().Be(validCoupon.Code);
            order.Coupon?.Amount.Should().Be(validCoupon.Amount);
            order.Coupon?.Percentage.Should().Be(validCoupon.Percentage);
        }

        [Fact(DisplayName = nameof(CreateOrderWithoutPaymentIdShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithoutPaymentIdShouldThrow()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var validCoupon = _orderTestFixture.ValidCoupon;
            var validProducts = _orderTestFixture.ValidProducts();

            var action = () => new Entity.Order(Guid.NewGuid(),
                                                address,
                                                shippingData,
                                                validProducts,
                                                Guid.Empty,
                                                validCoupon);

            action.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "PaymentId should not be empty.");
        }


        [Fact(DisplayName = nameof(CreateOrderWithInvalidAddressShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressShouldThrow()
        {
            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               null,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Address should not be empty.");
        }

        [Theory(DisplayName = nameof(CreateOrderWithInvalidAddressStreetShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressStreetShouldThrow(string invalidStreet)
        {
            Address address = new(_orderTestFixture.Faker.Address.Country(),
                                  _orderTestFixture.Faker.Address.StateAbbr(),
                                  _orderTestFixture.Faker.Address.City(),
                                  invalidStreet,
                                  _orderTestFixture.Faker.Address.BuildingNumber(),
                                  _orderTestFixture.Faker.Lorem.Letter(100),
                                  _orderTestFixture.Faker.Address.ZipCode());

            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Street should not be empty.");
        }

        [Theory(DisplayName = nameof(CreateOrderWithInvalidAddressCountryShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressCountryShouldThrow(string invalidCountry)
        {
            Address address = new(invalidCountry,
                                  _orderTestFixture.Faker.Address.StateAbbr(),
                                  _orderTestFixture.Faker.Address.City(),
                                  _orderTestFixture.Faker.Address.StreetName(),
                                  _orderTestFixture.Faker.Address.BuildingNumber(),
                                  _orderTestFixture.Faker.Lorem.Letter(100),
                                  _orderTestFixture.Faker.Address.ZipCode());

            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Country should not be empty.");
        }

        [Theory(DisplayName = nameof(CreateOrderWithInvalidAddressStateShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressStateShouldThrow(string invalidState)
        {
            Address address = new(_orderTestFixture.Faker.Address.Country(),
                                  invalidState,
                                  _orderTestFixture.Faker.Address.City(),
                                  _orderTestFixture.Faker.Address.StreetName(),
                                  _orderTestFixture.Faker.Address.BuildingNumber(),
                                  _orderTestFixture.Faker.Lorem.Letter(100),
                                  _orderTestFixture.Faker.Address.ZipCode());

            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "State should not be empty.");
        }

        [Theory(DisplayName = nameof(CreateOrderWithInvalidAddressZipCodeShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressZipCodeShouldThrow(string invalidZipCode)
        {
            Address address = new(_orderTestFixture.Faker.Address.Country(),
                                  _orderTestFixture.Faker.Address.StateAbbr(),
                                  _orderTestFixture.Faker.Address.City(),
                                  _orderTestFixture.Faker.Address.StreetName(),
                                  _orderTestFixture.Faker.Address.BuildingNumber(),
                                  _orderTestFixture.Faker.Lorem.Letter(100),
                                  invalidZipCode);

            var shippingData = _orderTestFixture.ValidShippingData;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "ZipCode should not be empty.");
        }

        [Theory(DisplayName = nameof(CreateOrderWithInvalidAddressCityShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressCityShouldThrow(string invalidCity)
        {
            Address address = new(_orderTestFixture.Faker.Address.Country(),
                                  _orderTestFixture.Faker.Address.StateAbbr(),
                                  invalidCity,
                                  _orderTestFixture.Faker.Address.StreetName(),
                                  _orderTestFixture.Faker.Address.BuildingNumber(),
                                  _orderTestFixture.Faker.Lorem.Letter(100),
                                  _orderTestFixture.Faker.Address.ZipCode());

            var shippingData = _orderTestFixture.ValidShippingData;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "City should not be empty.");
        }

        [Theory(DisplayName = nameof(CreateOrderWithInvalidAddressBuildingNumberShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressBuildingNumberShouldThrow(string invalidBuildingNumber)
        {
            Address address = new(_orderTestFixture.Faker.Address.Country(),
                                  _orderTestFixture.Faker.Address.StateAbbr(),
                                  _orderTestFixture.Faker.Address.City(),
                                  _orderTestFixture.Faker.Address.StreetName(),
                                  invalidBuildingNumber,
                                  _orderTestFixture.Faker.Lorem.Letter(100),
                                  _orderTestFixture.Faker.Address.ZipCode());

            var shippingData = _orderTestFixture.ValidShippingData;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "BuildingNumber should not be empty.");
        }

        [Fact(DisplayName = nameof(CreateOrderWithInvalidShippingDataShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidShippingDataShouldThrow()
        {
            var address = _orderTestFixture.ValidAddress;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               null,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "ShippingData should not be empty.");
        }

        [Theory(DisplayName = nameof(CreateOrderWithInvalidShippingDataDocumentShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidShippingDataDocumentShouldThrow(string invalidShippingDataCompanyDocument)
        {
            var address = _orderTestFixture.ValidAddress;
            ShippingData shippingData = new(_orderTestFixture.ValidProductPrice,
                                            invalidShippingDataCompanyDocument);

            var validProducts = _orderTestFixture.ValidProducts();

            var order = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               validProducts,
                                               Guid.NewGuid());


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "ShippingCompanyDocument should not be empty.");
        }

        [Theory(DisplayName = nameof(AddProductWithEmptyOrNullProductShouldThrowException))]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void AddProductWithEmptyOrNullProductShouldThrowException(List<Product> products)
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var validProducts = _orderTestFixture.ValidProducts();

            var action = () => new Entity.Order(Guid.NewGuid(),
                                               address,
                                               shippingData,
                                               products,
                                               Guid.NewGuid());


            action.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Products should not be empty.");
        }

        [Fact(DisplayName = nameof(AddValidProductToOrderShouldNotThrow))]
        [Trait("Domain", "Checkout")]
        public void AddValidProductToOrderShouldNotThrow()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var validProducts = _orderTestFixture.ValidProducts(1);

            var order = new Entity.Order(Guid.NewGuid(), 
                                        address,
                                        shippingData,
                                        validProducts,
                                        Guid.NewGuid());


            order.Products.Should().HaveCount(1);
        }

        [Fact(DisplayName = nameof(CreateProductWithPriceZeroShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateProductWithPriceZeroShouldThrow()
        {
            var price = _orderTestFixture.ValidProductPrice;

            var netPrice = _orderTestFixture.ValidProductNetPrice(price);

            var productCreation = () => new Entity.Product(0,
                                                           netPrice,
                                                           _orderTestFixture.ValidProductSku,
                                                           _orderTestFixture.ValidProductQuantity);

            productCreation.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "GrossPrice should be greater than 0.");
        }

        [Fact(DisplayName = nameof(CreateProductWithNetPriceZeroShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateProductWithNetPriceZeroShouldThrow()
        {
            var price = _orderTestFixture.ValidProductPrice;

            var productCreation = () => new Entity.Product(price,
                                                           0,
                                                           _orderTestFixture.ValidProductSku,
                                                           _orderTestFixture.ValidProductQuantity);

            productCreation.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "NetPrice should be greater than 0.");
        }

        [Fact(DisplayName = nameof(CreateProductWithNetPriceZeroShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateProductWithNetPriceGreaterThanGrossPriceShouldThrow()
        {
            var price = _orderTestFixture.ValidProductPrice;

            var netPrice = price + _orderTestFixture.ValidProductPrice;

            var productCreation = () => new Entity.Product(price,
                                                           netPrice,
                                                           _orderTestFixture.ValidProductSku,
                                                           _orderTestFixture.ValidProductQuantity);

            productCreation.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "NetPrice should be less than {0}.".FormatWith(price));
        }

        [Fact(DisplayName = nameof(CreateProductWithPriceZeroShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateProductWithQuantityZeroShouldThrow()
        {
            var price = _orderTestFixture.ValidProductPrice;

            var netPrice = _orderTestFixture.ValidProductNetPrice(price);

            var productCreation = () => new Entity.Product(price,
                                                           netPrice,
                                                           _orderTestFixture.ValidProductSku,
                                                           0);

            productCreation.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Quantity should be greater than 0.");
        }

        [Theory(DisplayName = nameof(CreateProductWithPriceZeroShouldThrow))]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        [Trait("Domain", "Checkout")]
        public void CreateProductWithEmptyOrNullSkuShouldThrow(string invalidSku)
        {
            var price = _orderTestFixture.ValidProductPrice;

            var netPrice = _orderTestFixture.ValidProductNetPrice(price);

            var productCreation = () => new Entity.Product(price,
                                                           netPrice,
                                                           invalidSku,
                                                           _orderTestFixture.ValidProductQuantity);

            productCreation.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Sku should not be empty.");
        }

        [Fact(DisplayName = nameof(TotalsShouldBeTotalsOfProducts))]
        [Trait("Domain", "Checkout")]
        public void TotalsShouldBeTotalsOfProducts()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;
            var validProducts = _orderTestFixture.ValidProducts();

            var order = new Entity.Order(Guid.NewGuid(),
                                        address,
                                        shippingData,
                                        validProducts,
                                        Guid.NewGuid());

            var netTotalPriceWithoutShippingPrice = validProducts.Sum(v => v.NetPrice);
            var grossTotalPriceWithoutShippingPrice = validProducts.Sum(v => v.GrossPrice);


            order.GrossTotalPrice.Should().BeGreaterThan(order.NetTotalPrice);
            (order.NetTotalPrice - order.ShippingData.Price).Should().Be(netTotalPriceWithoutShippingPrice);
            (order.GrossTotalPrice - order.ShippingData.Price).Should().Be(grossTotalPriceWithoutShippingPrice);
        }

        [Fact(DisplayName = nameof(CreatingOrderWithoutClientIdShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreatingOrderWithoutClientIdShouldThrow()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;
            var validProducts = _orderTestFixture.ValidProducts();

            var orderCreation = () => new Entity.Order(Guid.Empty,
                                                        address,
                                                        shippingData,
                                                        validProducts,
                                                        Guid.NewGuid());

            orderCreation.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "ClientId should not be empty.");
        }


        public static IEnumerable<object[]> InvalidPaymentData()
        {
            yield return new object[] { new List<Payment> { } };
            yield return new object[] { null };
        }
    }
}
