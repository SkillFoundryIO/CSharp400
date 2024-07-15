using System.ComponentModel.DataAnnotations;

namespace HelloMvc.Models
{
    public class Contact : IValidatableObject
    {
        public int ContactID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Country")]
        public string CountryCode { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            // user must be at least 18
            if (DoB.Date > DateTime.Today.AddYears(-18))
            {
                errors.Add(new ValidationResult("Contacts must be over 18!", ["DoB"]));
            }

            return errors;
        }
    }
}
