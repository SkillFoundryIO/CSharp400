using LibraryManagement.Application.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        /// <summary>
        /// List all media types
        /// </summary>
        /// <returns></returns>
        [HttpGet("types")]
        [ProducesResponseType(typeof(List<Borrower>), StatusCodes.Status200OK)]
        public IActionResult GetMediaTypes()
        {
            var result = _mediaService.GetMediaTypes();

            if (result.Ok)
                return Ok(result.Data);

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List all media by type
        /// </summary>
        /// <param name="mediaTypeId"></param>
        /// <returns></returns>
        [HttpGet("types/{mediaTypeId}")]
        [ProducesResponseType(typeof(List<Borrower>), StatusCodes.Status200OK)]
        public IActionResult ListMediaByType(int mediaTypeId)
        {
            var result = _mediaService.ListMedia(mediaTypeId);

            if (result.Ok)
                return Ok(result.Data);

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List the most popular media
        /// </summary>
        /// <returns></returns>
        [HttpGet("top")]
        [ProducesResponseType(typeof(List<Borrower>), StatusCodes.Status200OK)]
        public IActionResult GetMostPopularMedia()
        {
            var result = _mediaService.GetMostPopularMedia();

            if (result.Ok)
                return Ok(result.Data);

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List archived media
        /// </summary>
        /// <returns></returns>
        [HttpGet("archived")]
        [ProducesResponseType(typeof(List<Borrower>), StatusCodes.Status200OK)]
        public IActionResult GetArchivedMedia()
        {
            var result = _mediaService.GetArchivedMedia();

            if (result.Ok)
                return Ok(result.Data);

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add media
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMedia(Media media)
        {
            var result = _mediaService.AddMedia(media);

            if (result.Ok)
                return Created("", media);

            return StatusCode(500, result.Message);
        }


        /// <summary>
        /// Archive media
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost("{mediaId}/archive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult ArchiveMedia(int mediaId, Media media)
        {
            media.MediaID = mediaId;
            var result = _mediaService.ArchiveMedia(media);

            if (result.Ok)
                return NoContent();

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Edit media
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPut("{mediaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult EditMedia(int mediaId, Media media)
        {
            media.MediaID = mediaId;
            var result = _mediaService.EditMedia(media);

            if (result.Ok)
                return NoContent();

            return StatusCode(500, result.Message);
        }
    }
}
