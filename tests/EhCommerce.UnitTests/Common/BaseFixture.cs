using Bogus;

namespace EhCommerce.UnitTests.Common
{
    public abstract class BaseFixture
    {
        protected Faker Faker => new();
    }
}
