using LibraryManagement.API.Models;
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
        private readonly ILogger _logger;

        public MediaController(IMediaService mediaService,
            ILogger<MediaController> logger)
        {
            _mediaService = mediaService;
            _logger = logger;
        }

        /// <summary>
        /// List all media types
        /// </summary>
        /// <returns></returns>
        [HttpGet("types")]
        [ProducesResponseType(typeof(List<MediaType>), StatusCodes.Status200OK)]
        public IActionResult GetMediaTypes()
        {
            var result = _mediaService.GetMediaTypes();

            if (result.Ok)
            {
                _logger.LogInformation("Media types obtained successfully");
                return Ok(result.Data);
            }

            _logger.LogError("Error listing media types. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List all media by type
        /// </summary>
        /// <param name="mediaTypeId"></param>
        /// <returns></returns>
        [HttpGet("types/{mediaTypeId}")]
        [ProducesResponseType(typeof(List<Media>), StatusCodes.Status200OK)]
        public IActionResult ListMediaByType(int mediaTypeId)
        {
            var result = _mediaService.ListMedia(mediaTypeId);

            if (result.Ok)
            {
                _logger.LogInformation("Media by type obtained successfully");
                return Ok(result.Data);
            }

            _logger.LogError("Error listing media by type. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List the most popular media
        /// </summary>
        /// <returns></returns>
        [HttpGet("top")]
        [ProducesResponseType(typeof(List<TopMediaItem>), StatusCodes.Status200OK)]
        public IActionResult GetMostPopularMedia()
        {
            var result = _mediaService.GetMostPopularMedia();

            if (result.Ok)
            {
                _logger.LogInformation("Top media by obtained successfully");
                return Ok(result.Data);
            }

            _logger.LogError("Error listing top media. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// List archived media
        /// </summary>
        /// <returns></returns>
        [HttpGet("archived")]
        [ProducesResponseType(typeof(List<Media>), StatusCodes.Status200OK)]
        public IActionResult GetArchivedMedia()
        {
            var result = _mediaService.GetArchivedMedia();

            if (result.Ok)
            {
                _logger.LogInformation("Archived media by obtained successfully");
                return Ok(result.Data);
            }

            _logger.LogError("Error listing archived media. {Mesage}", result.Message);
            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Add media
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddMedia(AddMedia media)
        {
            if(ModelState.IsValid)
            {
                var entity = new Media
                {
                    Title = media.Title,
                    MediaTypeID = media.MediaTypeID
                };

                var result = _mediaService.AddMedia(entity);

                if (result.Ok)
                {
                    _logger.LogInformation("Media added successfully");
                    return Created("", entity);
                }

                _logger.LogError("Error adding media. {Mesage}", result.Message);
                return StatusCode(500, result.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        /// <summary>
        /// Archive media
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPost("{mediaId}/archive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ArchiveMedia(int mediaId, EditMedia media)
        {
            if (media.MediaID != mediaId)
            {
                ModelState.AddModelError("MediaID", "Mismatch between media object id and route id");
                return BadRequest(ModelState);
            }
            
            if(ModelState.IsValid)
            {
                var entity = new Media
                {
                    MediaID = media.MediaID,
                    MediaTypeID = media.MediaTypeID,
                    Title = media.Title,
                    IsArchived = media.IsArchived
                };

                var result = _mediaService.ArchiveMedia(entity);

                if (result.Ok)
                {
                    _logger.LogInformation("Media archived successfully");
                    return NoContent();
                }

                _logger.LogError("Error archiving media. {Mesage}", result.Message);
                return StatusCode(500, result.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Edit media
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        [HttpPut("{mediaId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditMedia(int mediaId, EditMedia media)
        {
            if (media.MediaID != mediaId)
            {
                ModelState.AddModelError("MediaID", "Mismatch between media object id and route id");
                return BadRequest(ModelState);
            }

            if(ModelState.IsValid)
            {
                var entity = new Media
                {
                    MediaID = media.MediaID,
                    MediaTypeID = media.MediaTypeID,
                    Title = media.Title,
                    IsArchived = media.IsArchived
                };

                var result = _mediaService.EditMedia(entity);

                if (result.Ok)
                {
                    _logger.LogInformation("Media updated successfully");
                    return NoContent();
                }

                _logger.LogError("Error updating media. {Mesage}", result.Message);
                return StatusCode(500, result.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
