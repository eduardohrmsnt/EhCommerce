using Bogus;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.UnitTests.Common
{
    public abstract class BaseFixture
    {
        protected Faker Faker => new();
    }
}
