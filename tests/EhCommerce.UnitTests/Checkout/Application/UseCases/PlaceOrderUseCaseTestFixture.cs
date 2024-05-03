using Bogus;
using Bogus.Extensions.Brazil;
using EhCommerce.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EhCommerce.UnitTests.Checkout.Application.UseCases
{
    [CollectionDefinition(nameof(PlaceOrderUseCaseTestFixture))]
    public class PlaceOrderUseCaseTestFixtureCollection : ICollectionFixture<PlaceOrderUseCaseTestFixture> { }
    public class PlaceOrderUseCaseTestFixture : BaseFixture
    {
        //public PlaceOrderInput ValidInput => new PlaceOrderInput
        //{        
        //    ShoppingCartId = Guid.NewGuid(),
        //    Address = new AddressPlaceOrderInput
        //    {
        //        Country = Faker.Address.Country(),
        //        State = Faker.Address.StateAbbr(),
        //        City = Faker.Address.City(),
        //        Street = Faker.Address.StreetName(),
        //        BuildingNumber = Faker.Address.BuildingNumber(),
        //        Description = Faker.Lorem.Letter(100),
        //        ZipCode = Faker.Address.ZipCode()
        //    },
        //    Shipping = new ShippingPlaceOrderInput
        //    {
        //        ShippingCompanyDocument = Faker.Company.Cnpj(),
        //        Price = Faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0)
        //    },
        //    PaymentData = new PaymentData
        //    {

        //    }
        //};
    }
}
