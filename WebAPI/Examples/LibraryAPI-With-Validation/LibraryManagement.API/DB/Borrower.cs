using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.DB
{
    public class Borrower : IValidatableObject
    {
        public int BorrowerID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (FirstName.Trim() != FirstName)
            {
                results.Add(new ValidationResult("First name cannot have leading or trailing whitespaces"));
            }

            if (LastName.Trim() != LastName)
            {
                results.Add(new ValidationResult("Last name cannot have leading or trailing whitespaces"));
            }

            if (!Email.Contains('@'))
            {
                results.Add(new ValidationResult("Email format is not valid"));
            }

            return results;
        }
    }
}
