using EhCommerce.Contracts.Checkout.PlaceOrder;
using EhCommerce.Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PlaceOrderInput : IUseCaseRequest<PlaceOrderOutput>
    {
        public Guid ShoppingCartId { get; set; }

        public AddressPlaceOrderInput? Address { get; set; }

        public ShippingDataPlaceOrderInput? ShippingData { get; set; }

        public PaymentPlaceOrderInput? Payment { get; set; }
    }
}
