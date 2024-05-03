using Bogus;
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

        [Theory(DisplayName = nameof(CreateOrderSuccess))]
        [MemberData(nameof(ValidPaymentData))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderSuccess(List<Payment> payments)
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var validCoupon = _orderTestFixture.ValidCoupon;

            var order = new Entity.Order(address,
                                        shippingData,
                                        payments,
                                        validCoupon);

            order.Should().NotBeNull();
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

            for (var i = 0; i < order.Payments.Count; i++)
            {
                if (order.Payments[i] is CreditCardPayment)
                {
                    ((CreditCardPayment)order.Payments[i]).CreditCardNumber.Should().Be(((CreditCardPayment)payments[i]).CreditCardNumber);
                    ((CreditCardPayment)order.Payments[i]).CardHolderName.Should().Be(((CreditCardPayment)payments[i]).CardHolderName);
                    ((CreditCardPayment)order.Payments[i]).ExpirationDate.Should().Be(((CreditCardPayment)payments[i]).ExpirationDate);
                }
                else if (order.Payments[i] is BilletPayment)
                {
                    ((BilletPayment)order.Payments[i]).BilletUrl.Should().Be(((BilletPayment)payments[i]).BilletUrl);
                }
                else if (order.Payments[i] is InstantPayment)
                {

                    ((InstantPayment)order.Payments[i]).RandomKey.Should().Be(((InstantPayment)payments[i]).RandomKey);
                }
            }

        }

        [Theory(DisplayName = nameof(CreateOrderWithoutPaymentShouldThrowError))]
        [MemberData(nameof(InvalidPaymentData))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithoutPaymentShouldThrowError(object invalidPayment)
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               (List<Payment>)invalidPayment);


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Payments should not be empty.");
        }


        [Fact(DisplayName = nameof(CreateOrderWithPaymentsOfTwoTypesShouldThrowError))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithPaymentsOfTwoTypesShouldThrowError()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;

            var invalidPayments = _orderTestFixture.TwoTypesOfPayment;

            var order = () => new Entity.Order(address,
                                        shippingData,
                                        invalidPayments);


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Only one type of payment is allowed.");
        }

        [Fact(DisplayName = nameof(CreateOrderWithInvalidAddressShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidAddressShouldThrow()
        {
            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;

            var order = () => new Entity.Order(null,
                                               shippingData,
                                               payments);


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

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               payments);


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

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               payments);


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

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               payments);


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
            var payments = _orderTestFixture.RandomPayment;

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               payments);


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
            var payments = _orderTestFixture.RandomPayment;

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               payments);


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
            var payments = _orderTestFixture.RandomPayment;

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               payments);


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "BuildingNumber should not be empty.");
        }

        [Fact(DisplayName = nameof(CreateOrderWithInvalidShippingDataShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateOrderWithInvalidShippingDataShouldThrow()
        {
            var address = _orderTestFixture.ValidAddress;
            var payments = _orderTestFixture.RandomPayment;

            var order = () => new Entity.Order(address,
                                               null,
                                               payments);


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

            var payments = _orderTestFixture.RandomPayment;

            var order = () => new Entity.Order(address,
                                               shippingData,
                                               payments);


            order.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "ShippingCompanyDocument should not be empty.");
        }

        [Fact(DisplayName = nameof(AddProductWithEmptyOrNullProductShouldThrowException))]
        [Trait("Domain", "Checkout")]
        public void AddProductWithEmptyOrNullProductShouldThrowException()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;

            var order = new Entity.Order(address,
                                        shippingData,
                                        payments);

            var action = () => order.AddItem(null);


            action.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Product should not be empty.");
        }

        [Fact(DisplayName = nameof(AddValidProductToOrderShouldNotThrow))]
        [Trait("Domain", "Checkout")]
        public void AddValidProductToOrderShouldNotThrow()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var coupon = _orderTestFixture.ValidCoupon;
            var payments = _orderTestFixture.RandomPayment;

            var order = new Entity.Order(address,
                                        shippingData,
                                        payments);

            order.AddItem(_orderTestFixture.ValidProduct());


            order.Products.Should().HaveCount(1);
        }

        [Fact(DisplayName = nameof(CreateProductWithPriceZeroShouldThrow))]
        [Trait("Domain", "Checkout")]
        public void CreateProductWithPriceZeroShouldThrow()
        {
            var price = _orderTestFixture.ValidProductPrice;

            var netPrice = _orderTestFixture.ValidNetPrice(price);

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

            var netPrice = _orderTestFixture.ValidNetPrice(price);

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

            var netPrice = _orderTestFixture.ValidNetPrice(price);

            var productCreation = () => new Entity.Product(price,
                                                           netPrice,
                                                           invalidSku,
                                                           _orderTestFixture.ValidProductQuantity);

            productCreation.Should().Throw<DomainException>().Which.ValidationResult.Should().Contain(vr => vr.Message == "Sku should not be empty.");
        }

        [Fact(DisplayName = nameof(AddValidProductToOrderShouldNotThrow))]
        [Trait("Domain", "Checkout")]
        public void AddDiscountCouponToOrderShouldRecalculateTotalValue()
        {
            var address = _orderTestFixture.ValidAddress;
            var shippingData = _orderTestFixture.ValidShippingData;
            var payments = _orderTestFixture.RandomPayment;

            var order = new Entity.Order(address,
                                        shippingData,
                                        payments);

            var product1 = _orderTestFixture.ValidProduct();

            var product2 = _orderTestFixture.ValidProduct();

            var product3 = _orderTestFixture.ValidProduct();

            var netTotalPriceWithoutShippingPrice = product1.NetPrice + product2.NetPrice + product3.NetPrice;
            var grossTotalPriceWithoutShippingPrice = product1.GrossPrice + product2.GrossPrice + product3.GrossPrice;

            order.AddItem(product1);
            order.AddItem(product2);
            order.AddItem(product3);

            order.GrossTotalPrice.Should().BeGreaterThan(order.NetTotalPrice);
            (order.NetTotalPrice - order.ShippingData.Price).Should().Be(netTotalPriceWithoutShippingPrice);
            (order.GrossTotalPrice - order.ShippingData.Price).Should().Be(grossTotalPriceWithoutShippingPrice);
        }


        public static IEnumerable<object[]> InvalidPaymentData()
        {
            yield return new object[] { new List<Payment> { } };
            yield return new object[] { null };
        }

        public static IEnumerable<object[]> ValidPaymentData()
        {
            var fixture = new OrderTestFixture();

            yield return new object[] { new List<Payment> { fixture.ValidBilletPayment } };
            yield return new object[] { new List<Payment> { fixture.ValidInstantPayment } };
            yield return new object[] { new List<Payment>
            {
                fixture.ValidCreditCardPayment,
                fixture.ValidCreditCardPayment
            }
            };
        }
    }
}
