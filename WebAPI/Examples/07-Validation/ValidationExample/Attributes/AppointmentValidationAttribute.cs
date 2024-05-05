using System.ComponentModel.DataAnnotations;
using ValidationExample.Models;

namespace ValidationExample.Attributes
{
    public class AppointmentValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if(value is Appointment3)
            {
                var model = value as Appointment3;
                
                // only allow past dates for our test account
                if(model.Date < DateTime.Today && model.CustomerEmail != "test@test.com")
                {
                    return new ValidationResult("Date cannot be in the past!");
                }

                return ValidationResult.Success;            
            }
            else
            {
                return new ValidationResult("This must be attached to an Appointment3 type.");
            }

        }
    }
}
