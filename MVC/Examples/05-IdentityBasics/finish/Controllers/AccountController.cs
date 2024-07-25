using IdentityBasics.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityBasics.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            LoginForm model = new();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForm model)
        {
            var account = AccountRepository.Login(model.UserName, model.Password);

            if (account == null)
            {
                TempData["message"] = "Invalid credentials, try again!";
            }
            else
            {
                var identity = new ClaimsIdentity(account.Claims, Settings.WebCookieName);
                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14)
                };

                await HttpContext.SignInAsync(Settings.WebCookieName, principal, props);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Settings.WebCookieName);
            return RedirectToAction("Index", "Home");
        }
    }
}
