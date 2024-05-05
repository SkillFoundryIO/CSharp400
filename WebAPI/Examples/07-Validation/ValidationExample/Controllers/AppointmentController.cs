using Microsoft.AspNetCore.Mvc;
using ValidationExample.Models;

namespace ValidationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Appointment>> GetAppointments(DateTime date)
        {
            if (date < DateTime.Now)
            {
                ModelState.AddModelError("date", "Appointment date must be in the future.");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new List<Appointment>()
            {
                new Appointment
                {
                    CustomerEmail = "test@test.com",
                    Date = date,
                    Subject = "Installation"
                }
            });
        }
    }
}
