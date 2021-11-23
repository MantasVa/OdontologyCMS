using System;
using System.ComponentModel.DataAnnotations;

namespace Odontology.Web.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string errorMessage;
        private readonly DateTime dateToCompareTo = DateTime.UtcNow;
        public DateGreaterThanAttribute(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value is not DateTime)
            {
                throw new InvalidOperationException("Using attribute not on Datetime property");
            }

            var dateTime = (DateTime) value;

            return dateTime > dateToCompareTo ? ValidationResult.Success : new ValidationResult(errorMessage);
        }
    }
}
