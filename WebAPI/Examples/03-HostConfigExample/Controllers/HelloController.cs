using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HelloAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        [EnableCors(Constants.AllowAnyGetOrigins)]
        public int[] GetNumbers()
        {
            return [1, 3, 5, 7, 9];
        }
    }
}
