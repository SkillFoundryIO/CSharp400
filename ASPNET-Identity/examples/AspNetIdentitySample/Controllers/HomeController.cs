using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentitySample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
