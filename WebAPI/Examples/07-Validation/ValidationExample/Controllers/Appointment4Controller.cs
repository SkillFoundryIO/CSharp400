using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationExample.Models;

namespace ValidationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Appointment4Controller : ControllerBase
    {
        [HttpPost]
        public ActionResult<Appointment4> CreateAppointment(Appointment4 appointment)
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
