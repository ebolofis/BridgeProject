using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.CustomAnotations
{
    /// <summary>
    /// Check if a number is grater than zero
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            var num = Convert.ToDecimal(value);
            if (num <= 0) return new ValidationResult(this.FormatErrorMessage(string.Format(Hit.Services.Resources.Errors.GREATERTHANZERO, validationContext.DisplayName)));
            return ValidationResult.Success;
        }
    }
}
