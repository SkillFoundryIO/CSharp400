using LibraryManagement.API.DB;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private LibraryContext _context;

        public BorrowerController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Borrower>> GetAll()
        {
            try
            {
                var borrowers = _context.Borrower.ToList();
                return Ok(borrowers);
            }
            catch (Exception ex)
            {
                // TODO: log the real exception information
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Borrower> GetBorrower(int id)
        {
            try
            {
                var borrower = _context.Borrower.Find(id);

                if (borrower == null)
                {
                    return NotFound();
                }

                return Ok(borrower);
            }
            catch (Exception ex)
            {
                // TODO: log the real exception information
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Borrower> PostBorrower(Borrower borrower)
        {
            try
            {
                _context.Borrower.Add(borrower);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetBorrower), new { id = borrower.BorrowerID }, borrower);
            }
            catch (Exception ex)
            {
                // TODO: log the real exception information
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBorrower(int id, Borrower borrower)
        {
            if (id != borrower.BorrowerID)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(borrower);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO: log the real exception information
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Borrower> DeleteBorrower(int id)
        {
            try
            {
                var borrower = _context.Borrower.Find(id);
                if (borrower == null)
                {
                    return NotFound();
                }

                _context.Borrower.Remove(borrower);
                _context.SaveChanges();

                return Ok(borrower);
            }
            catch (Exception ex)
            {
                // TODO: log the real exception information
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }
        }
    }
}
