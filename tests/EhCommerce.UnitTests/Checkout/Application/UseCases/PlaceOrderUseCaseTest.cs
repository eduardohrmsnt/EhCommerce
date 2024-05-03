using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EhCommerce.UnitTests.Checkout.Application.UseCases
{
    [Collection(nameof(PlaceOrderUseCaseTestFixture))]
    public class PlaceOrderUseCaseTest
    {
        private readonly PlaceOrderUseCaseTestFixture _fixture;

        public PlaceOrderUseCaseTest(PlaceOrderUseCaseTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(PlaceOrderWithAllCorrectShouldPersistOrder))]
        [Trait("UseCases", "PlaceOrderUseCase")]
        public async Task PlaceOrderWithAllCorrectShouldPersistOrder()
        {
            //var input = _fixture.ValidInput;
        }
    }
}
