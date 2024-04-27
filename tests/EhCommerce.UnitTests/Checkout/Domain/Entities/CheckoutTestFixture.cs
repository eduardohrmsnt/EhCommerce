using Bogus.Extensions.Brazil;
using EhCommerce.Checkout.ValueObjects;
using EhCommerce.UnitTests.Common;
using Xunit;

namespace EhCommerce.UnitTests.Checkout.Domain.Entities
{
    [CollectionDefinition(nameof(CheckoutTestFixture))]
    public class CheckoutTestFixtureCollection : ICollectionFixture<CheckoutTestFixture> { }
    public class CheckoutTestFixture : BaseFixture
    {
        public Address ValidAddress => new Address(Faker.Address.Country(),
                                                       Faker.Address.StateAbbr(),
                                                       Faker.Address.City(),
                                                       Faker.Address.StreetName(),
                                                       Faker.Address.BuildingNumber(),
                                                       Faker.Lorem.Letter(100),
                                                       Faker.Address.ZipCode());

        public ShippingData ValidShippingData => new ShippingData(Faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0),
                                                                  Faker.Company.Cnpj());

        public Payment ValidCreditCardPayment => new CreditCardPayment(Faker.Finance.CreditCardNumber(),
                                                                       Faker.Person.FullName.ToUpper(),
                                                                       string.Concat(Faker.Date.Month(), "/", Faker.Date.Future().Year));


        public Payment ValidBilletPayment => new BilletPayment(Faker.Internet.Url());

        public Payment ValidInstantPayment => new InstantPayment(Faker.Lorem.Paragraph());

        public Coupon ValidCoupon => new Coupon(Faker.Lorem.Letter(num: 10).ToUpper(),
                                                Faker.Random.Int(min: 1, max: 90),
                                                Faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0));

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
    }
}
