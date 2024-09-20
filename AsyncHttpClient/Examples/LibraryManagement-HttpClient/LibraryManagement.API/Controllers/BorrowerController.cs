using LibraryManagement.API.Models;
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
        private readonly ILogger _logger;

        public BorrowerController(IBorrowerService borrowerService,
            ILogger<BorrowerController> logger)
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddBorrower(AddBorrower borrower)
        {
            if(ModelState.IsValid)
            {
                var entity = new Borrower
                {
                    FirstName = borrower.FirstName,
                    LastName = borrower.LastName,
                    Email = borrower.Email,
                    Phone = borrower.Phone
                };

                var result = _borrowerService.AddBorrower(entity);

                if (result.Ok)
                {
                    _logger.LogInformation("Successfully added a borrower");
                    return Created("", entity);
                }

                if (result.Message.Contains("Borrower with email"))
                {
                    _logger.LogWarning(result.Message);
                    return Conflict(result.Message);
                }

                _logger.LogError("Error adding a borrower. {Mesage}", result.Message);
                return StatusCode(500, result.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Edit a borrower
        /// </summary>
        /// <param name="id"></param>
        /// <param name="borrower"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditBorrower(int id, EditBorrower borrower)
        {
            if(borrower.BorrowerID != id)
            {
                ModelState.AddModelError("id", "Mismatch between borrower object id and route id");
                return BadRequest(ModelState);
            }

            if(ModelState.IsValid)
            {
                var entity = new Borrower
                {
                    BorrowerID = borrower.BorrowerID,
                    FirstName = borrower.FirstName,
                    LastName = borrower.LastName,
                    Email = borrower.Email,
                    Phone = borrower.Phone
                };

                var result = _borrowerService.EditBorrower(entity);

                if (result.Ok)
                {
                    _logger.LogInformation($"Successfully updated {entity.Email}");
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
            else
            {
                return BadRequest(ModelState);
            }    
        }

        /// <summary>
        /// Delete a borrower
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult DeleteBorrower(int id)
        {
            var result = _borrowerService.DeleteBorrower(new Borrower { BorrowerID = id });

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
