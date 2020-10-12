using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Aibidia.API.Common.Attributes
{
    public class RequiredIfAttribute : RequiredAttribute
    {
        private string _compareToPropertyName { get; }
        private object[] _desiredValues { get; }

        public RequiredIfAttribute(string compareToPropertyName, params object[] desiredValues) : base()
        {
            _compareToPropertyName = compareToPropertyName;
            _desiredValues = desiredValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            PropertyInfo property = type.GetProperty(_compareToPropertyName);
            var compareToPropertyValue = property.GetValue(instance);

            // Require 'value' when the target property's value and desired value is null
            // Require 'value' when the target property's value is the same as the desired value
            if (_desiredValues == null && compareToPropertyValue == null)
            {
                ValidationResult result = base.IsValid(value, validationContext);
                return result;
            }

            foreach (object desiredValue in _desiredValues)
            {
                if (compareToPropertyValue != null && compareToPropertyValue.Equals(desiredValue))
                {
                    ValidationResult result = base.IsValid(value, validationContext);
                    return result;
                }
            }

            return ValidationResult.Success;
        }
    }
}
