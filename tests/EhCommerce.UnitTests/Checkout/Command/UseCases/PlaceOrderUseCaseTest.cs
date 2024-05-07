using EhCommerce.Checkout.Command.UseCases.PlaceOrder;
using Entities = EhCommerce.Checkout.Entities;
using EhCommerce.Contracts.Payment.MakePayment;
using EhCommerce.Contracts.ShoppingCart.GetShoppingCartById;
using EhCommerce.Contracts.Stock.ProductsHasStock;
using EhCommerce.Contracts.Stock.ReserveStockForOrder;
using FluentAssertions;
using Moq;
using Xunit;
using EhCommerce.Shared.Domain;

namespace EhCommerce.UnitTests.Checkout.Command.UseCases
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
            var input = _fixture.ValidInput;

            var getShoppingCartByIdUseCaseMock = _fixture.GetShoppingCartByIdUseCaseMock;
            var productsHasStockUseCaseMock = _fixture.ProductsHasStockUseCaseMock;
            var makePaymentUseCaseMock = _fixture.MakePaymentUseCaseMock;
            var reserveStockForOrderUseCaseMock = _fixture.ReserveStockForOrderUseCaseMock;
            var orderRepositoryMock = _fixture.OrderRepositoryMock;

            var makePaymentOutput = _fixture.ValidMakePaymentOutput(input.Payment.PaymentType);

            getShoppingCartByIdUseCaseMock.Setup(g => g.Handle(It.IsAny<GetShoppingCartByIdInput>(),
                                                           CancellationToken.None)).ReturnsAsync(_fixture.ValidShoppingCartGenericModel(input.ShoppingCartId));

            makePaymentUseCaseMock.Setup(g => g.Handle(It.IsAny<MakePaymentInput>(),
                                               CancellationToken.None)).ReturnsAsync(makePaymentOutput);


            productsHasStockUseCaseMock.Setup(p => p.Handle(It.IsAny<ProductsHasStockInput>(),
                                                           CancellationToken.None)).ReturnsAsync(new ProductsHasStockOutput { AnyWithoutStock = false });


            orderRepositoryMock.Setup(c => c.Insert(It.IsAny<Entities.Order>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());

            var useCase = new PlaceOrderUseCase(getShoppingCartByIdUseCaseMock.Object,
                                                productsHasStockUseCaseMock.Object,
                                                makePaymentUseCaseMock.Object,
                                                reserveStockForOrderUseCaseMock.Object,
                                                orderRepositoryMock.Object);

            var result = await useCase.Handle(input, CancellationToken.None);


            makePaymentUseCaseMock.Verify(c => c.Handle(It.IsAny<MakePaymentInput>(),
                                                        It.IsAny<CancellationToken>()), Times.Once());
            reserveStockForOrderUseCaseMock.Verify(c => c.Handle(It.IsAny<ReserveStockForOrderInput>(),
                                                                 It.IsAny<CancellationToken>()), Times.Once());
            orderRepositoryMock.Verify(c => c.Insert(It.IsAny<Entities.Order>(), It.IsAny<CancellationToken>()), Times.Once());
            result.OrderId.Should().NotBeEmpty();
            result.PaymentData.BilletUrl.Should().Be(makePaymentOutput.BilletUrl);
            result.PaymentData.InstantPaymentCode.Should().Be(makePaymentOutput.InstantPaymentCode);
            result.PaymentData.PaymentId.Should().Be(makePaymentOutput.PaymentId);
            result.PaymentData.PaymentType.Should().Be(makePaymentOutput.PaymentType);
            result.PaymentData.PaymentStatus.Should().Be(makePaymentOutput.PaymentStatus);
        }

        [Fact(DisplayName = nameof(PlaceOrderWithProductOutOfStockShouldThrow))]
        [Trait("UseCases", "PlaceOrderUseCase")]
        public async Task PlaceOrderWithProductOutOfStockShouldThrow()
        {
            var input = _fixture.ValidInput;

            var getShoppingCartByIdUseCaseMock = _fixture.GetShoppingCartByIdUseCaseMock;
            var productsHasStockUseCaseMock = _fixture.ProductsHasStockUseCaseMock;
            var makePaymentUseCaseMock = _fixture.MakePaymentUseCaseMock;
            var reserveStockForOrderUseCaseMock = _fixture.ReserveStockForOrderUseCaseMock;
            var orderRepositoryMock = _fixture.OrderRepositoryMock;

            var makePaymentOutput = _fixture.ValidMakePaymentOutput(input.Payment.PaymentType);

            getShoppingCartByIdUseCaseMock.Setup(g => g.Handle(It.IsAny<GetShoppingCartByIdInput>(),
                                                           CancellationToken.None)).ReturnsAsync(_fixture.ValidShoppingCartGenericModel(input.ShoppingCartId));

            makePaymentUseCaseMock.Setup(g => g.Handle(It.IsAny<MakePaymentInput>(),
                                               CancellationToken.None)).ReturnsAsync(makePaymentOutput);


            productsHasStockUseCaseMock.Setup(p => p.Handle(It.IsAny<ProductsHasStockInput>(),
                                                           CancellationToken.None)).ReturnsAsync(new ProductsHasStockOutput { AnyWithoutStock = true });


            orderRepositoryMock.Setup(c => c.Insert(It.IsAny<Entities.Order>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());

            var useCase = new PlaceOrderUseCase(getShoppingCartByIdUseCaseMock.Object,
                                                productsHasStockUseCaseMock.Object,
                                                makePaymentUseCaseMock.Object,
                                                reserveStockForOrderUseCaseMock.Object,
                                                orderRepositoryMock.Object);

            var action = async () => await useCase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<DomainException>().WithMessage("We apologize for the inconvenience, but some products are out of stock at the moment. We are working to replenish our inventory as quickly as possible.");
        }
    }
}
