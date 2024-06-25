using HelloMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloMvc.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult List()
        {
            var model = new List<Contact>
            {
                new Contact { ContactID=1, Name="Joe Schmoe", Email="jschmoe@example.com"},
                new Contact { ContactID=2, Name="Jane Doe", Email="jdoe@example.com"},
                new Contact { ContactID=3, Name="Dwayne Robinson", Email="drobinson@example.com"},
            };

            return View(model);
        }
    }
}
