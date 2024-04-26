using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                                                       Faker.Random.Words(3));

        public Payment ValidCreditCardPayment => new CreditCardPayment(Faker.Address.Country(),
                                                                       Faker.Address.StateAbbr(),
                                                                       Faker.Address.City(),
                                                                       Faker.Address.StreetName(),
                                                                       Faker.Address.BuildingNumber(),
                                                                       Faker.Random.Words(3));


        public Payment ValidBilletPayment => new BilletPayment(Faker.Address.Country(),
                                                                       Faker.Address.StateAbbr(),
                                                                       Faker.Address.City(),
                                                                       Faker.Address.StreetName(),
                                                                       Faker.Address.BuildingNumber(),
                                                                       Faker.Random.Words(3));

        public Payment ValidBilletPayment => new InstantPayment(Faker.Random.Guid(),
                                                                Faker.Random.Decimal(min: (decimal)0.0));
    }
}
