using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SnippetsDotNet5MVC.Extensions
{
    public class ValidPhoneNumberAttribute : ValidationAttribute
    {
        public ValidPhoneNumberAttribute() //You can pass a parameter here
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMess = string.Empty;
            bool isValid = true;
            if ((value as string).Trim().Length != 13)
            {
                errorMess = $"Phone number must contain {13} digits.";
                isValid = false;
            }

            if (!(value as string).Trim().StartsWith(263.ToString()))
            {
                errorMess = $"Phone number must start with {263}.";
                isValid = false;
            }

            if (!long.TryParse((value as string).Trim(), out long temp))
            {
                errorMess = $"Phone number contains some non-numeric characters.";
                isValid = false;
            }

            if (!isValid)
            {
                return new ValidationResult(errorMess);
            }

            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "iszimphonenumber",
            };
            yield return rule;
        }
    }
}
