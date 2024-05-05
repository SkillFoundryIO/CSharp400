using System.ComponentModel.DataAnnotations;

namespace ValidationExample.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                DateTime date = Convert.ToDateTime(value);

                if (date <= DateTime.Today)
                {
                    return new ValidationResult("Date must be in the future");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult(
                    "This attribute only works with DateTime objects");
            }
        }
    }
}
