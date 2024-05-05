using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Models
{
    public class AddBorrower : IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be correctly formatted")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Phone number must be correctly formatted")]
        public string Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (FirstName.Any(char.IsWhiteSpace))
            {
                results.Add(new ValidationResult("White spaces are not allowed in the first name"));
            }

            if (FirstName.Any(char.IsDigit))
            {
                results.Add(new ValidationResult("Digits are not allowed in the first name"));
            }

            if (LastName.Any(char.IsWhiteSpace))
            {
                results.Add(new ValidationResult("White spaces are not allowed in the last name"));
            }

            if (LastName.Any(char.IsDigit))
            {
                results.Add(new ValidationResult("Digits are not allowed in the last name"));
            }

            return results;
        }
    }
}
