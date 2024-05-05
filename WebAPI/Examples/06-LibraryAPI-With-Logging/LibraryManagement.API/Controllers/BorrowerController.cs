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
            _logger.LogDebug("GetAll() method invoked.");

            try
            {
                var borrowers = _repository.GetAll();

                _logger.LogInformation("Successfully retrieved borrowers.");
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
            _logger.LogDebug("GetBorrower() method invoked.");

            try
            {
                var borrower = _repository.GetById(id);

                if (borrower == null)
                {
                    _logger.LogWarning("Logger with id {id} not found", id);
                    return NotFound();
                }

                _logger.LogInformation("Successfully retrieved a borrower.");
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
        public ActionResult<Borrower> PostBorrower(Borrower borrower)
        {
            _logger.LogDebug("PostBorrower() method invoked.");

            try
            {
                _repository.Add(borrower);

                _logger.LogInformation("Successfully added a borrower.");

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
            _logger.LogDebug("UpdateBorrower() method invoked.");

            if (id != borrower.BorrowerID)
            {
                _logger.LogWarning("Id {Id} provided in the path doesn't match the borrower id.", id);
                return BadRequest();
            }

            try
            {
                _repository.Update(borrower);

                _logger.LogInformation("Successfully updated a borrower.");
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
            _logger.LogDebug("DeleteBorrower() method invoked.");

            try
            {
                var borrower = _repository.GetById(id);
                if (borrower == null)
                {
                    return NotFound();
                }

                _repository.Delete(borrower);

                _logger.LogInformation("Successfully deleted a borrower.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting a borrower");
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }
        }
    }
}
