using EhCommerce.Language;
using EhCommerce.Shared.Domain;
using EhCommerce.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EhCommerce.Shared.Validator
{
    public static class Validator
    {
        public static List<ValidationResult> Contract => new();

        public static List<ValidationResult> ShouldNotBeEmpty(this List<ValidationResult> validationResults,
                                                       string fieldName,
                                                       string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                validationResults.AddValidationResult(fieldName,
                                                      DomainMessages.ObrigatoryField.FormatWith(fieldName));

            return validationResults;
        }

        public static List<ValidationResult> ShouldNotBeNull(this List<ValidationResult> validationResults,
                                                          string fieldName,
                                                          object value)
        {
            if (value is null)
                AddValidationResult(validationResults,
                                    fieldName,
                                    DomainMessages.ObrigatoryField.FormatWith(fieldName));

            return validationResults;
        }

        public static List<ValidationResult> ShouldBeFalse(this List<ValidationResult> validationResults,
                                          string fieldName,
                                          bool value,
                                          string message)
        {
            if (value == true)
                AddValidationResult(validationResults,
                                    fieldName,
                                    message);

            return validationResults;
        }

        public static List<ValidationResult> ShouldHaveItems<T>(this List<ValidationResult> validationResults,
                                                  string fieldName,
                                                  List<T> value)
        {
            if (value is null || value.Count == 0)
                AddValidationResult(validationResults,
                                    fieldName,
                                    DomainMessages.ObrigatoryField.FormatWith(fieldName));

            return validationResults;
        }

        public static List<ValidationResult> ShouldBeGreaterThan(this List<ValidationResult> validationResults,
                                                                 string fieldName,
                                                                 decimal value,
                                                                 decimal comparer)
        {
            if (value <= comparer)
                AddValidationResult(validationResults,
                                    fieldName,
                                    DomainMessages.FieldShouldBeGreaterThan.FormatWith(fieldName, comparer));

            return validationResults;
        }

        public static List<ValidationResult> ShouldBeLessThan(this List<ValidationResult> validationResults,
                                                         string fieldName,
                                                         decimal value,
                                                         decimal comparer)
        {
            if (value >= comparer)
                AddValidationResult(validationResults,
                                    fieldName,
                                    DomainMessages.FieldShouldBeLessThan.FormatWith(fieldName, comparer));

            return validationResults;
        }

        public static void Validate(this List<ValidationResult> validationResults)
        {
            if (validationResults.Count > 0)
                throw new DomainException(validationResults);
        }

        public static void AddValidationResult(this List<ValidationResult> validationResults,
                                         string fieldName,
                                         string message)
        {
            validationResults.Add(new ValidationResult(fieldName,
                                                       message));
        }
    }
}
