using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Aibidia.API.Common.Attributes
{
    public class GreaterOrEqualsAttribute : CompareAttribute
    {
        public GreaterOrEqualsAttribute(string otherProperty) : base(otherProperty)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Property {OtherProperty} does not exist for object.");
            }

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (value != null && otherPropertyValue != null)
            {
                var thisValue = value as IComparable;
                var compareValue = otherPropertyValue as IComparable;
                var result = thisValue.CompareTo(compareValue);

                if (result < 0)
                {
                    return new ValidationResult($"Value is not greater or equal than {OtherProperty}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
