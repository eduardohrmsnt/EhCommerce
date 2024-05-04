using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PaymentDataPlaceOrderInput
    {
        public string? CreditCardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpirationDate { get; set; }
        public string? CVV { get; set; }
    }
}
