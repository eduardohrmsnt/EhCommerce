using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Shared.Validator
{
    public class ValidationResult
    {
        public ValidationResult(string fieldName,
                                string message)
        {
            FieldName = fieldName;
            Message = message;
        }

        public string FieldName { get; set; }

        public string Message { get; set; }
    }
}
