using Microsoft.AspNetCore.Mvc;

namespace HelloAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public int[] GetNumbers()
        {
            return [1, 3, 5, 7, 9];
        }
    }
}
