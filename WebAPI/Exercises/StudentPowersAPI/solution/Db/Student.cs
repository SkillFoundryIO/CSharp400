using System.ComponentModel.DataAnnotations;

namespace StudentPowersAPI.Db
{
    public class Student : IValidatableObject
    {
        public int StudentID { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Alias { get; set; }
        
        public DateTime DoB { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var span = DateTime.Today.Year - DoB.Year;
            if (span < 13 || span > 18)
            {
                errors.Add(new ValidationResult("You must be between 13 and 18 to enroll in the academy.", ["DoB"]));
            }

            return errors;
        }
    }
}
