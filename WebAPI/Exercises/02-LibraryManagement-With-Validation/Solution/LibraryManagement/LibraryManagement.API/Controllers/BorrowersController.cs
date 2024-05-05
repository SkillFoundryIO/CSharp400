using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;
        private readonly ILogger _logger;

        public BorrowersController(IBorrowerService borrowerService,
            ILogger<BorrowersController> logger)
        {
            _borrowerService = borrowerService;
            _logger = logger;
        }

        /// <summary>
        /// List all borrowers
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<Borrower>), StatusCodes.Status200OK)]
        public IActionResult GetAllBorrowers()
        {
            var result = _borrowerService.GetAllBorrowers();

            if (result.Ok)
            {
                _logger.LogInformation("Successfully retrieved borrowers.");
                return Ok(result.Data);
            }

            _logger.LogError("Error retrieving borrowers. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// View a borrower
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(Borrower), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBorrower(string email)
        {
            var result = _borrowerService.GetBorrower(email);

            if (result.Ok)
            {
                _logger.LogInformation("Successfully retrieved a borrower.");
                return Ok(result.Data);
            }
                
            if (result.Message.Contains("Borrower with email"))
            {
                _logger.LogWarning(result.Message);
                return NotFound(result.Message);
            }
                

            _logger.LogError("Error retrieving a borrower. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add a borrower
        /// </summary>
        /// <param name="borrower"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddBorrower([FromBody] Borrower borrower)
        {
            var result = _borrowerService.AddBorrower(borrower);

            if (result.Ok)
            {
                _logger.LogInformation("Successfully added a borrower");
                return Created();
            }        

            if (result.Message.Contains("Borrower with email"))
            {
                _logger.LogWarning(result.Message);
                return Conflict(result.Message);
            }

            _logger.LogError("Error adding a borrower. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Edit a borrower
        /// </summary>
        /// <param name="borrowerId"></param>
        /// <param name="borrower"></param>
        /// <returns></returns>
        [HttpPut("{borrowerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult EditBorrower(int borrowerId, [FromBody] Borrower borrower)
        {
            borrower.BorrowerID = borrowerId;

            var result = _borrowerService.EditBorrower(borrower);

            if (result.Ok)
            {
                _logger.LogInformation("Successfully updated a borrower");
                return NoContent();
            }
                

            if (result.Message.Contains("Borrower with email"))
            {
                _logger.LogWarning(result.Message);
                return Conflict(result.Message);
            }
                

            _logger.LogError("Error updating a borrower. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Delete a borrower
        /// </summary>
        /// <param name="borrowerId"></param>
        /// <returns></returns>
        [HttpDelete("{borrowerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteBorrower(int borrowerId)
        {
            var borrower = new Borrower
            {
                BorrowerID = borrowerId,
            };

            var result = _borrowerService.DeleteBorrower(borrower);

            if (result.Ok)
            {
                _logger.LogInformation("Successfully deleted a borrower");
                return NoContent();
            }   

            _logger.LogError("Error deleting a borrower. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }
    }
}
