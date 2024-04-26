using LibraryManagement.API.DB;
using LibraryManagement.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerRepository _repository;
        private readonly ILogger _logger;

        public BorrowerController(IBorrowerRepository repository,
            ILogger<BorrowerController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the full list of borrowers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Borrower>), StatusCodes.Status200OK)]
        public ActionResult<List<Borrower>> GetAll()
        {
            try
            {
                var borrowers = _repository.GetAll();
                return Ok(borrowers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving borrowers");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a borrower by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Borrower), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Borrower> GetBorrower(int id)
        {
            try
            {
                var borrower = _repository.GetById(id);

                if (borrower == null)
                {
                    return NotFound();
                }

                return Ok(borrower);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving a borrower");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts a new borrower
        /// </summary>
        /// <param name="borrower"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Borrower> PostBorrower([FromBody] Borrower borrower)
        {
            try
            {
                _repository.Add(borrower);

                return CreatedAtAction(nameof(PostBorrower), new { id = borrower.BorrowerID }, borrower);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting a borrower");
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates details of a borrower
        /// </summary>
        /// <param name="id"></param>
        /// <param name="borrower"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateBorrower(int id, Borrower borrower)
        {
            if (id != borrower.BorrowerID)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(borrower);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting a borrower");
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a borrower
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Borrower> DeleteBorrower(int id)
        {
            try
            {
                var borrower = _repository.GetById(id);
                if (borrower == null)
                {
                    return NotFound();
                }

                _repository.Delete(borrower);

                return Ok(borrower);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting a borrower");
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }
        }
    }
}
