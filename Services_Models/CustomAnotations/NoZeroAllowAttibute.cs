using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Services.Models.CustomAnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NoZeroAllowAttibute : ValidationAttribute
    {
        // public decimal CheckValue { get; set; }

        public NoZeroAllowAttibute()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var chkValue = this.CheckValue;
            //if ((decimal)value == 0)
            if (decimal.Parse(value.ToString()) == 0)
            {
                //return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName + "." + "No zero value allow"));
            }
            return null;
        }

    }
}
