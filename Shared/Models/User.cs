using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CREATIM_naloga.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Not a valid E-mail address.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public bool Bussiness { get; set; } = false;
        [RequiredIf("Bussiness", true, ErrorMessage = "Tax number is required for bussiness accounts.")]
        [StringLengthIf("Bussiness", true, 8, 8, ErrorMessage = "Tax number must be 8 characters long.")]
        [RegularExpressionIf("Bussiness", true, @"^[0-9]*$", ErrorMessage = "Only input digits 0-9.")]
        public string TaxNumber { get; set; } = string.Empty;
        public Group Group { get; set; } = new Group();
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        public string OtherProperty { get; private set; }
        public object OtherPropertyValue { get; private set; }

        public RequiredIfAttribute(string otherProperty, object otherPropertyValue)
        {
            this.OtherProperty = otherProperty;
            this.OtherPropertyValue = otherPropertyValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherProperty = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult(
                    string.Format(CultureInfo.CurrentCulture, "Could not find a property named '{0}'.", this.OtherProperty));
            }

            object otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            if (object.Equals(otherValue, this.OtherPropertyValue))
            {
                if (value == null)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }

                string val = value as string;
                if (val != null && val.Trim().Length == 0)
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class StringLengthIfAttribute : ValidationAttribute
    {
        public string OtherProperty { get; private set; }
        public object OtherPropertyValue { get; private set; }
        public int MaximumLength { get; private set; }
        public int MinimumLength { get; private set; }

        public StringLengthIfAttribute(string otherProperty, object otherPropertyValue, int maximumLength, int minimumLength = 0)
        {
            this.OtherProperty = otherProperty;
            this.OtherPropertyValue = otherPropertyValue;
            this.MaximumLength = maximumLength;
            this.MinimumLength = minimumLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherProperty = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult(
                    string.Format(CultureInfo.CurrentCulture, "Could not find a property named '{0}'.", this.OtherProperty));
            }

            object otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            if (object.Equals(otherValue, this.OtherPropertyValue))
            {
                string val = value as string;
                if (val != null && (val.Length > this.MaximumLength || val.Length < this.MinimumLength))
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RegularExpressionIfAttribute : ValidationAttribute
    {
        public string OtherProperty { get; private set; }
        public object OtherPropertyValue { get; private set; }
        public string Pattern { get; private set; }

        public RegularExpressionIfAttribute(string otherProperty, object otherPropertyValue, string pattern)
        {
            this.OtherProperty = otherProperty;
            this.OtherPropertyValue = otherPropertyValue;
            this.Pattern = pattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherProperty = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult(
                    string.Format(CultureInfo.CurrentCulture, "Could not find a property named '{0}'.", this.OtherProperty));
            }

            object otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            if (object.Equals(otherValue, this.OtherPropertyValue))
            {
                string val = value as string;
                if (val != null && !Regex.IsMatch(val, this.Pattern))
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }

}
