using Bogus;

namespace EhCommerce.UnitTests.Common
{
    public abstract class BaseFixture
    {
        public Faker Faker => new();
    }
}
