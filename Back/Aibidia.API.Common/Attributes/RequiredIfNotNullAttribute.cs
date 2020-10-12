using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Aibidia.API.Common.Attributes
{
    public class RequiredIfNotNullAttribute : RequiredAttribute
    {
        private string _compareToPropertyName { get; }

        public RequiredIfNotNullAttribute(string compareToPropertyName) : base()
        {
            _compareToPropertyName = compareToPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(_compareToPropertyName);
            var compareToPropertyValue = property.GetValue(instance);

            // If compareToPropertyValue is not null, then value is required
            if (compareToPropertyValue != null)
            {
                ValidationResult result = base.IsValid(value, validationContext);
                return result;
            }

            return ValidationResult.Success;
        }
    }
}
