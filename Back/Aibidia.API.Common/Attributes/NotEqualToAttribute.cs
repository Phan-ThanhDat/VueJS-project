using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aibidia.API.Common.Attributes
{
    public class NotEqualToAttribute : ValidationAttribute
    {
        private string _notEqualToProperty { get; }
        private bool _compareEmpty { get; }
        
        public NotEqualToAttribute(string notEqualToProperty, bool compareEmpty = false)
        {
            if (notEqualToProperty == null)
            {
                throw new ArgumentNullException("notEqualToProperty");                
            }

            _notEqualToProperty = notEqualToProperty;
            _compareEmpty = compareEmpty;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return true;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo notEqualToPropertyInfo = validationContext.ObjectType.GetProperty(_notEqualToProperty);
            if (notEqualToPropertyInfo == null)
            {
                return new ValidationResult($"Property {_notEqualToProperty} does not exist for object.");
            }

            var notEqualToPropertyValue = notEqualToPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var shouldCompare = _compareEmpty || (!_compareEmpty && notEqualToPropertyValue != null && value != null);
            
            if (shouldCompare && Equals(notEqualToPropertyValue, value))
            {
                return new ValidationResult($"Value cannot be equal to {_notEqualToProperty}.");
            }

            return ValidationResult.Success;       
        }
    }
}
