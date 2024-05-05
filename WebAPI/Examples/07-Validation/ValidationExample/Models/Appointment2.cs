using System.ComponentModel.DataAnnotations;
using ValidationExample.Attributes;

namespace ValidationExample.Models
{
    public class Appointment2
    {
        [Required(ErrorMessage="The appointment date is required.")]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        [FutureDate]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings=false)]
        public string Subject { get; set; }
    }
}
