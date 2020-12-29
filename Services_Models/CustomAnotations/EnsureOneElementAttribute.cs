using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.CustomAnotations
{
    /// <summary>
    /// Check if a List has at least one element
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList;
            if (list != null)
            {
                if (list.Count == 0) return new ValidationResult(this.FormatErrorMessage(Hit.Services.Resources.Errors.LISTONEELEMENT));
            }
            return ValidationResult.Success;
        }
    }
}
