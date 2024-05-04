using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class ShippingDataPlaceOrderInput
    {
        public string? ShippingCompanyDocument { get; set; }

        public decimal Price { get; set; }
    }
}
