using EhCommerce.Checkout.ValueObjects;
using FluentAssertions;
using Xunit;
using Entity = EhCommerce.Checkout.Entities;

namespace EhCommerce.UnitTests.Checkout.Domain.Entities
{
    [Collection(nameof(CheckoutTestFixture))]
    public class CheckoutTest
    {
        private readonly CheckoutTestFixture _checkoutTestFixture;

        public CheckoutTest(CheckoutTestFixture checkoutTestFixture)
        {
            _checkoutTestFixture = checkoutTestFixture;
        }

        [Fact(DisplayName = nameof(CreateCheckoutSuccess))]
        [Trait("Domain", "Checkout")]
        public void CreateCheckoutSuccess()
        {
            var address = _checkoutTestFixture.ValidAddress;
            var shippingData = _checkoutTestFixture.ValidShippingData;
            var validCoupon = _checkoutTestFixture.ValidCoupon;
            var payments = _checkoutTestFixture.RandomPayment;

            var checkout = new Entity.Checkout(address,
                                        shippingData,
                                        payments,
                                        validCoupon);

            checkout.Should().NotBeNull();
            checkout.Address.Street.Should().Be(address.Street);
            checkout.Address.State.Should().Be(address.State);
            checkout.Address.City.Should().Be(address.City);
            checkout.Address.Country.Should().Be(address.Country);
            checkout.Address.BuildingNumber.Should().Be(address.BuildingNumber); 
            checkout.Address.ZipCode.Should().Be(address.ZipCode);
            checkout.Address.Description.Should().Be(address.Description);
            checkout.ShippingData.ShippingCompanyDocument.Should().Be(shippingData.ShippingCompanyDocument);
            checkout.ShippingData.Amount.Should().Be(shippingData.Amount);
            checkout.Coupon.Code.Should().Be(validCoupon.Code);
            checkout.Coupon.Amount.Should().Be(validCoupon.Amount);
            checkout.Coupon.Percentage.Should().Be(validCoupon.Percentage);

            for (var i = 0; i < checkout.Payments.Count; i++)
            {
                if (checkout.Payments[i] is CreditCardPayment)
                {
                    ((CreditCardPayment)checkout.Payments[i]).CreditCardNumber.Should().Be(((CreditCardPayment)payments[i]).CreditCardNumber);
                    ((CreditCardPayment)checkout.Payments[i]).CardHolderName.Should().Be(((CreditCardPayment)payments[i]).CardHolderName);
                    ((CreditCardPayment)checkout.Payments[i]).ExpirationDate.Should().Be(((CreditCardPayment)payments[i]).ExpirationDate);
                }
                else if (checkout.Payments[i] is BilletPayment)
                {
                    ((BilletPayment)checkout.Payments[i]).BilletUrl.Should().Be(((BilletPayment)payments[i]).BilletUrl);
                }
                else if (checkout.Payments[i] is InstantPayment)
                {

                    ((InstantPayment)checkout.Payments[i]).RandomKey.Should().Be(((InstantPayment)payments[i]).RandomKey);
                }
            }

        }
    }
}
