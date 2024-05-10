using EhCommerce.Checkout.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.UnitTests.Common
{
    public class BaseCatalogFixture : BaseFixture
    {
        public decimal ValidProductPrice => Convert.ToDecimal(Faker.Commerce.Price(min: (decimal)0.1, max: 10000));

        public decimal ValidProductNetPrice(decimal price) => Convert.ToDecimal(Faker.Commerce.Price(min: (decimal)0.1, max: price));

        public int ValidProductQuantity => Faker.Random.Int(min: 1);

        public string ValidProductSku => string.Concat(Faker.Commerce.ProductAdjective().ToUpper(), Faker.Commerce.Color().ToUpper(), Faker.Lorem.Letter(1).ToUpper());

    }
}
