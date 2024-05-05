using Microsoft.AspNetCore.Mvc;
using ValidationExample.Models;

namespace ValidationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Appointment2Controller : ControllerBase
    {
        [HttpPost]
        public ActionResult<Appointment2> CreateAppointment(Appointment2 appointment)
        {
            if(ModelState.IsValid)
            {
                return Created("", appointment);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
