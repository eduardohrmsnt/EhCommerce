using EhCommerce.Contracts.Checkout.PlaceOrder;
using EhCommerce.Enums;
using EhCommerce.Shared.Application;

namespace EhCommerce.Contracts.Payment.MakePayment
{
    public class MakePaymentInput : IUseCaseRequest<MakePaymentOutput>
    {
        public PaymentType PaymentType { get; set; }

        public List<MakePaymentCreditCardModel>? CreditCardInformation { get; set; }

        public static MakePaymentInput FromPlaceOrderInput(PlaceOrderInput request)
        {
            return new MakePaymentInput
            {
                PaymentType = request.Payment.PaymentType,
                CreditCardInformation = request.Payment.Data?.Select(c =>
                {
                    return new MakePaymentCreditCardModel
                    {
                        CreditCardNumber = c.CreditCardNumber,
                        CardHolderName = c.CardHolderName,
                        ExpirationDate = c.ExpirationDate,
                        CVV = c.CVV,
                        CreditCardAmount = c.CreditCardAmount
                    };
                })?.ToList()
            };
        }
    }
}
