using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class MediaService : IMediaService
    {
        private IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public Result AddMedia(Media media)
        {
            try
            {
                _mediaRepository.Add(media);
                return ResultFactory.Success(media);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result ArchiveMedia(Media media)
        {
            try
            {
                media.IsArchived = true;
                _mediaRepository.Update(media);
                return ResultFactory.Success(media);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditMedia(Media media)
        {
            try
            {
                _mediaRepository.Update(media);
                return ResultFactory.Success(media);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<TopMediaItem>> GetMostPopularMedia()
        {
            try
            {
                var items = _mediaRepository.GetTopMedia();

                return ResultFactory.Success(items.OrderByDescending(i => i.CheckoutCount).Take(3).ToList());
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<TopMediaItem>>(ex.Message);
            }
        }

        public Result<List<Media>> ListMedia(int mediaTypeID)
        {
            try
            {
                return ResultFactory.Success(_mediaRepository.GetMediaByType(mediaTypeID));
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<Media>> GetArchivedMedia()
        {
            try
            {
                return ResultFactory.Success(_mediaRepository.GetAll().Where(m=>m.IsArchived).ToList());
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<MediaType>> GetMediaTypes()
        {
            try
            {
                return ResultFactory.Success(_mediaRepository.GetMediaTypes());
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<MediaType>>(ex.Message);
            }
        }
    }
}
