using EhCommerce.Shared.Domain;

namespace EhCommerce.Checkout.ValueObjects
{
    public abstract class Payment : IValueObject
    {
        protected decimal ValuePaid { get; set; }
    }
}
