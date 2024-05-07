using Bogus;
using Bogus.Extensions.Brazil;
using EhCommerce.Checkout.Interfaces;
using EhCommerce.Contracts.Checkout.PlaceOrder;
using EhCommerce.Contracts.Payment.MakePayment;
using EhCommerce.Contracts.ShoppingCart.GetShoppingCartById;
using EhCommerce.Contracts.Stock.ProductsHasStock;
using EhCommerce.Contracts.Stock.ReserveStockForOrder;
using EhCommerce.Enums;
using EhCommerce.UnitTests.Common;
using Moq;
using Xunit;

namespace EhCommerce.UnitTests.Checkout.Command.UseCases
{
    [CollectionDefinition(nameof(PlaceOrderUseCaseTestFixture))]
    public class PlaceOrderUseCaseTestFixtureCollection : ICollectionFixture<PlaceOrderUseCaseTestFixture> { }
    public class PlaceOrderUseCaseTestFixture : BaseFixture
    {
        public Mock<IGetShoppingCartByIdUseCase> GetShoppingCartByIdUseCaseMock => new();

        public Mock<IReserveStockForOrderUseCase> ReserveStockForOrderUseCaseMock => new();

        public Mock<IProductsHasStockUseCase> ProductsHasStockUseCaseMock => new();

        public Mock<IMakePaymentUseCase> MakePaymentUseCaseMock => new();

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
                        CVV = Faker.Finance.CreditCardCvv(),
                        CreditCardAmount = Faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0)
                    }
                }
            }
        };

        public Mock<IOrderRepository> OrderRepositoryMock => new();

        public ShoppingCartGenericModelOutput ValidShoppingCartGenericModel(Guid? shoppingCartId = null)
        {
            return new ShoppingCartGenericModelOutput
            {
                Id = shoppingCartId.GetValueOrDefault(Guid.NewGuid()),
                Products = new Faker<ShoppingCartProductModelOutput>().Rules((faker, model) => {
                    model.Quantity = faker.Random.Int(min: 1, max: 10);
                    model.ProductId = faker.Random.Guid();
                    model.GrossPrice = faker.Random.Decimal(min: (decimal)0.1, max: (decimal)99.0);
                    model.NetPrice = faker.Random.Decimal(min: (decimal)0.1, max: model.GrossPrice);
                    model.Sku = string.Concat(Faker.Commerce.ProductAdjective().ToUpper(), Faker.Commerce.Color().ToUpper(), Faker.Lorem.Letter(1).ToUpper());
                }).Generate(3)
            };
        }

        public MakePaymentOutput ValidMakePaymentOutput(PaymentType paymentType)
        {
            return new MakePaymentOutput
            {
                PaymentStatus = Faker.Random.Enum<PaymentStatus>(),
                PaymentType = paymentType,
                BilletUrl = paymentType == PaymentType.Billet ? Faker.Internet.Url() : null,
                InstantPaymentCode = paymentType == PaymentType.Billet ? Faker.Finance.BitcoinAddress() : null,
                PaymentId = Faker.Random.Guid()
            };
        }
    }
}
