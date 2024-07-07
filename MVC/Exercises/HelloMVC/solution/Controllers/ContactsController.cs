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

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ContactForm();

            model.Countries = new SelectList(CountryList.Countries, "Code", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactForm model)
        {
            ContactRepository.Add(model.Contact);

            TempData["message"] = $"Contact created with ID {model.Contact.ContactID}";
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = new Contact();

            model = ContactRepository.GetById(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteContact(int id)
        {
            ContactRepository.Delete(id);

            TempData["message"] = $"Contact ID {id} has been deleted.";
            return RedirectToAction("List");
        }
    }
}
