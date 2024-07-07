using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelloMvc.Models
{
    public class ContactForm
    {
        public Contact Contact { get; set; }
        public SelectList Countries { get; set; }
    }
}
