using Microsoft.AspNetCore.Mvc;
using TokenAPI.Models;
using TokenAPI.Services;

namespace TokenAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login(Credentials model)
        {
            var user = AccountRepository.Login(model.UserName, model.Password);

            if (user != null)
            {
                var token = _jwtService.GenerateToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
