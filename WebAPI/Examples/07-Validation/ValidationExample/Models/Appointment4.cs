using System.ComponentModel.DataAnnotations;

namespace ValidationExample.Models
{
    public class Appointment4 : IValidatableObject
    {
        [Required(ErrorMessage = "The appointment date is required.")]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if(Date < DateTime.Now)
            {
                errors.Add(new ValidationResult("Date must be in the future.", ["Date"]));
            }

            // Garfield famously hates mondays
            if(Date.DayOfWeek == DayOfWeek.Monday && CustomerEmail=="garfield@gmail.com")
            {
                errors.Add(new ValidationResult("We won't deal with Garfield on a Monday", ["Date", "CustomerEmail"]));
            }

            return errors;
        }
    }
}
