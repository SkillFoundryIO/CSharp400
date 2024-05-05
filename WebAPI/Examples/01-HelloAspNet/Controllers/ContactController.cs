using HelloAspNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            var contacts = new List<Contact>
            {
                new Contact { Name="John Doe", Email="jdoe@example.com"},
                new Contact { Name="Jane Smith", Email="jsmith@example.com"}
            };

            return contacts;
        }
    }
}
