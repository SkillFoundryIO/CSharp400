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

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
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
                return Ok(result.Data);

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
                return Ok(result.Data);

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Checkout a media item
        /// </summary>
        /// <returns></returns>
        [HttpPost("media/{mediaId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Checkout(int mediaId, [FromBody] string borrowerEmail)
        {
            var result = _checkoutService.Checkout(mediaId, borrowerEmail);

            if (result.Ok)
                return Created();

            if (result.Message.Contains("Borrower not found!"))
                return NotFound(result.Message);

            if (result.Message.Contains("This borrower "))
                return Conflict(result.Message);

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
                return NoContent();

            if (result.Message.Contains("This item is no longer checked out!"))
                return Conflict(result.Message);

            return StatusCode(500, result.Message);
        }
    }
}
