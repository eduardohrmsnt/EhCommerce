using Bogus;
using Bogus.Extensions.Brazil;
using EhCommerce.Checkout.Application.Enum;
using EhCommerce.Contracts.Checkout.PlaceOrder;
using EhCommerce.UnitTests.Common;
using Xunit;

namespace EhCommerce.UnitTests.Checkout.Application.UseCases
{
    [CollectionDefinition(nameof(PlaceOrderUseCaseTestFixture))]
    public class PlaceOrderUseCaseTestFixtureCollection : ICollectionFixture<PlaceOrderUseCaseTestFixture> { }
    public class PlaceOrderUseCaseTestFixture : BaseFixture
    {
        public PlaceOrderInput ValidInput => new PlaceOrderInput
        {
            ShoppingCartId = Guid.NewGuid(),
            Address = new AddressPlaceOrderInput
            {
                Country = Faker.Address.Country(),
                State = Faker.Address.StateAbbr(),
                City = Faker.Address.City(),
                Street = Faker.Address.StreetName(),
                BuildingNumber = Faker.Address.BuildingNumber(),
                Description = Faker.Lorem.Letter(100),
                ZipCode = Faker.Address.ZipCode()
            },
            ShippingData = new ShippingDataPlaceOrderInput
            {
                ShippingCompanyDocument = Faker.Company.Cnpj(),
                Price = Faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0)
            },
            Payment = new PaymentPlaceOrderInput
            {
                PaymentType = Faker.Random.Enum<PaymentType>(),
                Data = new List<PaymentDataPlaceOrderInput>()
                {
                    new PaymentDataPlaceOrderInput
                    {
                        CreditCardNumber = Faker.Finance.CreditCardNumber(),
                        CardHolderName = Faker.Person.FullName.ToUpper(),
                        ExpirationDate = string.Concat(Faker.Date.Month(), "/", Faker.Date.Future().Year),
                        CVV = Faker.Finance.CreditCardCvv()
                    }
                }
            }
        };
    }
}
