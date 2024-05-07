using EhCommerce.Contracts.Payment.MakePayment;
using EhCommerce.Enums;

namespace EhCommerce.Contracts.Checkout.PlaceOrder
{
    public class PaymentDataPlaceOrderOutput
    {
        public string BilletUrl { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public PaymentType PaymentType { get; set; }

        public string InstantPaymentCode { get; set; }

        public Guid PaymentId { get; set; }
        

        public static PaymentDataPlaceOrderOutput FromMakePaymentOutput(MakePaymentOutput paymentOutput)
        {
            return new PaymentDataPlaceOrderOutput
            {
                PaymentType = paymentOutput.PaymentType,
                PaymentStatus = paymentOutput.PaymentStatus,
                PaymentId = paymentOutput.PaymentId,
                BilletUrl = paymentOutput.BilletUrl,
                InstantPaymentCode = paymentOutput.InstantPaymentCode,
            };
        }
    }
}
