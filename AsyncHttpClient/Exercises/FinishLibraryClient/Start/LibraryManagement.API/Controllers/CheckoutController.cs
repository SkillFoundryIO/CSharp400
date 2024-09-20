using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;
        private readonly ILogger _logger;

        public CheckoutController(ICheckoutService checkoutService,
            ILogger<CheckoutController> logger)
        {
            _checkoutService = checkoutService;
            _logger = logger;
        }

        /// <summary>
        /// List available media
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<Media>), StatusCodes.Status200OK)]
        public IActionResult GetAvailableMedia()
        {
            var result = _checkoutService.GetAvailableMedia();

            if (result.Ok)
            {
                _logger.LogInformation("Available media by obtained successfully");
                return Ok(result.Data);
            }

            _logger.LogError("Error obtaining available media. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List checked out media
        /// </summary>
        /// <returns></returns>
        [HttpGet("log")]
        [ProducesResponseType(typeof(List<CheckoutLog>), StatusCodes.Status200OK)]
        public IActionResult GetCheckoutLog()
        {
            var result = _checkoutService.GetCheckedoutMedia();

            if (result.Ok)
            {
                _logger.LogInformation("Checked out media by obtained successfully");
                return Ok(result.Data);
            }

            _logger.LogError("Error obtaining checkout log. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Checkout a media item
        /// </summary>
        /// <returns></returns>
        [HttpPost("media/{mediaId}/{borrowerEmail}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Checkout(int mediaId, string borrowerEmail)
        {
            var result = _checkoutService.Checkout(mediaId, borrowerEmail);

            if (result.Ok)
            {
                _logger.LogInformation("Media checked out successfully");
                return Created();
            }

            _logger.LogWarning(result.Message);

            if (result.Message.Contains("Borrower not found!"))
            {
                return NotFound(result.Message);
            }
            
            if (result.Message.Contains("This borrower "))
            {
                return Conflict(result.Message);
            } 

            _logger.LogError("Error checking media out. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Return a media item
        /// </summary>
        /// <returns></returns>
        [HttpPost("returns/{checkoutLogId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Return(int checkoutLogId)
        {
            var result = _checkoutService.Return(checkoutLogId);

            if (result.Ok)
            {
                _logger.LogInformation("Successfully processed item return");
                return NoContent();
            }

            if (result.Message.Contains("This item is no longer checked out!"))
            {
                _logger.LogWarning(result.Message);
                return Conflict(result.Message);
            }  

            _logger.LogError("Error returning a media item. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }
    }
}
