using EhCommerce.Shared.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
