using EhCommerce.Checkout.Entities;
using EhCommerce.Checkout.Interfaces;
using EhCommerce.Checkout.ValueObjects;
using EhCommerce.Contracts.Checkout.PlaceOrder;
using EhCommerce.Contracts.Payment.MakePayment;
using EhCommerce.Contracts.ShoppingCart.GetShoppingCartById;
using EhCommerce.Contracts.Stock.ProductsHasStock;
using EhCommerce.Contracts.Stock.ReserveStockForOrder;
using EhCommerce.Shared.Domain;

namespace EhCommerce.Checkout.Command.UseCases.PlaceOrder
{
    public class PlaceOrderUseCase : IPlaceOrderUseCase
    {
        private readonly IGetShoppingCartByIdUseCase _getShoppingCartByIdUseCase;
        private readonly IProductsHasStockUseCase _productsHasStockUseCase;
        private readonly IMakePaymentUseCase _makePaymentUseCase;
        private readonly IReserveStockForOrderUseCase _reserveStockForOrderUseCase;
        private readonly IOrderRepository _orderRepository;

        public PlaceOrderUseCase(IGetShoppingCartByIdUseCase getShoppingCartByIdUseCase,
                                 IProductsHasStockUseCase productsHasStockUseCase,
                                 IMakePaymentUseCase makePaymentUseCase,
                                 IReserveStockForOrderUseCase reserveStockForOrderUseCase,
                                 IOrderRepository orderRepository)
        {
            _getShoppingCartByIdUseCase = getShoppingCartByIdUseCase;
            _productsHasStockUseCase = productsHasStockUseCase;
            _makePaymentUseCase = makePaymentUseCase;
            _reserveStockForOrderUseCase = reserveStockForOrderUseCase;
            _orderRepository = orderRepository;
        }

        public async Task<PlaceOrderOutput> Handle(PlaceOrderInput request,
                                                   CancellationToken cancellationToken)
        {
            var shoppingCart = await _getShoppingCartByIdUseCase.Handle(GetShoppingCartByIdInput.FromPlaceOrderInput(request),
                                                                        cancellationToken);

            var productsHasStockOutput = await _productsHasStockUseCase.Handle(ProductsHasStockInput.FromShoppingCartGenericModel(shoppingCart),
                                                                 cancellationToken);

            if (productsHasStockOutput.AnyWithoutStock is true)
                throw new DomainException("We apologize for the inconvenience, but some products are out of stock at the moment. We are working to replenish our inventory as quickly as possible.");

            var paymentOutput = await _makePaymentUseCase.Handle(MakePaymentInput.FromPlaceOrderInput(request),
                                                                 cancellationToken);

            var order = new Order(new Address(request.Address.Country,
                                              request.Address.State,
                                              request.Address.City,
                                              request.Address.Street,
                                              request.Address.BuildingNumber,
                                              request.Address.Description,
                                              request.Address.ZipCode),
                                  new ShippingData(request.ShippingData.Price,
                                                   request.ShippingData.ShippingCompanyDocument),
                                  new List<Product>(GetProductFromShoppingCart(shoppingCart)),
                                  paymentOutput.PaymentId,
                                  shoppingCart.Coupon is not null ?
                                      new Coupon(shoppingCart.Coupon.Code,
                                                 shoppingCart.Coupon.Percentage,
                                                 shoppingCart.Coupon.Amount) :
                                                 null);

            var orderId = await _orderRepository.Insert(order, cancellationToken);

            await _reserveStockForOrderUseCase.Handle(ReserveStockForOrderInput.FromOrder(order),
                                                      cancellationToken);

            return new PlaceOrderOutput
            {
                OrderId = orderId,
                PaymentData = PaymentDataPlaceOrderOutput.FromMakePaymentOutput(paymentOutput)
            };
        }

        private IEnumerable<Product> GetProductFromShoppingCart(ShoppingCartGenericModelOutput shoppingCart)
        {
            foreach (var product in shoppingCart.Products)
                yield return new Product(product.GrossPrice,
                                         product.NetPrice,
                                         product.Sku,
                                         product.Quantity);
        }
    }
}
