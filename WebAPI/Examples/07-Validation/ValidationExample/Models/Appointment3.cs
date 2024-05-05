using ValidationExample.Attributes;

namespace ValidationExample.Models
{
    [AppointmentValidation]
    public class Appointment3
    {
        public string CustomerEmail { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
    }
}
