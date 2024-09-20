using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowerController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
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
                return Ok(result.Data);

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
                return Ok(result.Data);

            if (result.Message.Contains("Borrower with email"))
                return NotFound(result.Message);

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
        public IActionResult AddBorrower(Borrower borrower)
        {
            var result = _borrowerService.AddBorrower(borrower);

            if (result.Ok)
                return Created("", borrower);

            if (result.Message.Contains("Borrower with email"))
                return Conflict(result.Message);

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
        public IActionResult EditBorrower(int borrowerId, Borrower borrower)
        {
            borrower.BorrowerID = borrowerId;

            var result = _borrowerService.EditBorrower(borrower);

            if (result.Ok)
                return NoContent();

            if (result.Message.Contains("Borrower with email"))
                return Conflict(result.Message);

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Delete a borrower
        /// </summary>
        /// <param name="borrowerId"></param>
        /// <returns></returns>
        [HttpDelete("{borrowerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteBorrower(int borrowerId)
        {
            var borrower = new Borrower
            {
                BorrowerID = borrowerId,
            };

            var result = _borrowerService.DeleteBorrower(borrower);

            if (result.Ok)
                return NoContent();

            return StatusCode(500, result.Message);
        }
    }
}
