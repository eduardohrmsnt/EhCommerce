using Bogus.Extensions.Brazil;
using EhCommerce.Checkout.Entities;
using EhCommerce.Checkout.ValueObjects;
using EhCommerce.UnitTests.Common;
using Xunit;

namespace EhCommerce.UnitTests.Checkout.Domain.Entities
{
    [CollectionDefinition(nameof(OrderTestFixture))]
    public class CheckoutTestFixtureCollection : ICollectionFixture<OrderTestFixture> { }
    public class OrderTestFixture : BaseCatalogFixture
    {
        public Address ValidAddress => new(Faker.Address.Country(),
                                           Faker.Address.StateAbbr(),
                                           Faker.Address.City(),
                                           Faker.Address.StreetName(),
                                           Faker.Address.BuildingNumber(),
                                           Faker.Lorem.Letter(100),
                                           Faker.Address.ZipCode());

        public ShippingData ValidShippingData => new(Faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0),
                                                     Faker.Company.Cnpj());

        public Payment ValidCreditCardPayment => new CreditCardPayment(Faker.Finance.CreditCardNumber(),
                                                                       Faker.Person.FullName.ToUpper(),
                                                                       string.Concat(Faker.Date.Month(), "/", Faker.Date.Future().Year));


        public Payment ValidBilletPayment => new BilletPayment(Faker.Internet.Url());

        public Payment ValidInstantPayment => new InstantPayment(Faker.Lorem.Paragraph());

        public Coupon ValidCoupon => new(Faker.Lorem.Letter(num: 10).ToUpper(),
                                         Faker.Random.Int(min: 1, max: 90),
                                         Faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0));


        public List<Product> ValidProducts(int quantity = 3)
        {
            return Enumerable.Range(0, quantity).Select(c => ValidProduct()).ToList();
        }

        public Product ValidProduct()
        {
            var price = ValidProductPrice;

            return new(price,
                       ValidProductNetPrice(price),
                       ValidProductSku,
                       ValidProductQuantity);
        }

        public List<Payment> RandomPayment => Enumerable.Range(0, 1).Select(c =>
        {
            var random = new Random();

            var randomNumber = random.Next(3);

            if (randomNumber == 1)
                return ValidInstantPayment;
            else if (randomNumber == 2)
                return ValidCreditCardPayment;
            else if (randomNumber == 3)
                return ValidBilletPayment;

            return ValidInstantPayment;

        }).ToList();

        public List<Payment> TwoTypesOfPayment => new List<Payment>
        {
            ValidBilletPayment,
            ValidInstantPayment
        };
    }
}
