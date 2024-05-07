using EhCommerce.Shared.Validator;

namespace EhCommerce.Shared.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string? message) : base(message)
        {
        }

        public DomainException(List<ValidationResult> validations) : base("An error ocurred.")
        {
            ValidationResult = validations;
        }

        public List<ValidationResult>? ValidationResult { get; }
    }
}
