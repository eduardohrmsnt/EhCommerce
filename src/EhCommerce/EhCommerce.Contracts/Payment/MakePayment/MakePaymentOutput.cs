using EhCommerce.Enums;

namespace EhCommerce.Contracts.Payment.MakePayment
{
    public class MakePaymentOutput
    {
        public string BilletUrl { get; set; }

        public string InstantPaymentCode { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public Guid PaymentId { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
