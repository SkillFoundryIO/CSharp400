using HelloMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RadioButtonExample()
        {
            var model = new RadioButtons();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RadioButtonExample(RadioButtons model)
        {
            return View(model);
        }

        public IActionResult CheckboxListExample()
        {
            var model = new CheckBoxList();
            model.SelectedCountries = CountryList.Countries.Select(c => new CheckBoxItem
            {
                ID = c.Code,
                Text = c.Name,
                Selected = false
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckboxListExample(CheckBoxList model)
        {
            return View(model);
        }
    }
}
