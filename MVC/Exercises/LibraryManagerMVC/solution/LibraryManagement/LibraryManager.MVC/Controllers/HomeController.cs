using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
