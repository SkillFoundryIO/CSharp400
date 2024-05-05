using Microsoft.AspNetCore.Mvc;
using ValidationExample.Models;

namespace ValidationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Appointment3Controller : ControllerBase
    {
        [HttpPost]
        public ActionResult<Appointment3> CreateAppointment(Appointment3 appointment)
        {
            if (ModelState.IsValid)
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
