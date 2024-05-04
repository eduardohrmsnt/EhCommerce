using EhCommerce.Checkout.Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PaymentPlaceOrderInput
    {
        public List<PaymentDataPlaceOrderInput>? Data { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
