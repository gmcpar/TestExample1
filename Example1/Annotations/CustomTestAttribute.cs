using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Annotations
{
    public class CustomTestAttribute : ValidationAttribute
    {
        public CustomTestAttribute() : base("{0} a custom")
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}