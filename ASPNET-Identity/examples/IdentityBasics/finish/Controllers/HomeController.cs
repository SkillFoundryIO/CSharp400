using IdentityBasics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace IdentityBasics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserData();

            model.Name = User.Identity?.Name ?? "Anonymous";
            model.IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
            model.Claims = User.Claims;

            return View(model);
        }

        [Authorize]
        public IActionResult UsersOnly()
        {
            return View();
        }

        [Authorize("Admin")]
        public IActionResult AdminOnly()
        {
            return View();
        }

        [Authorize("AdultOnly")]
        public IActionResult AdultsOnly()
        {
            var claim = User.FindFirst(ClaimTypes.DateOfBirth);
            var dob = DateTime.Parse(claim.Value);

            // This is an over 40 club
            if (dob > DateTime.Today.AddYears(-40))
            {
                TempData["message"] = "Adults only!";
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
