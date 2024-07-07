using HelloMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelloMvc.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult List()
        {
            var model = ContactRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new ContactForm();

            model.Contact = ContactRepository.GetById(id);
            model.Countries = new SelectList(CountryList.Countries, "Code", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ContactForm model)
        {
            ContactRepository.Update(model.Contact);

            TempData["message"] = "Contact Updated!";
            return RedirectToAction("List");
        }
    }
}
