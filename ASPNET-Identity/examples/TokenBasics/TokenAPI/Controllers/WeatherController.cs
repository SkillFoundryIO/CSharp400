using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenAPI.Models;

namespace TokenAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("forecast")]
        public IActionResult Index()
        {
            return Ok(WeatherRepository.GetSampleData());
        }
    }
}
