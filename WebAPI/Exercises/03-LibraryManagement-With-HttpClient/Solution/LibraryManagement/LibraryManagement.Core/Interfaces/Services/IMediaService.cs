using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IMediaService
    {
        Result<List<Media>> ListMedia(int mediaTypeID);
        Result AddMedia(Media media);
        Result EditMedia(Media media);
        Result ArchiveMedia(Media media);
        Result<List<Media>> GetArchivedMedia();
        Result<List<TopMediaItem>> GetMostPopularMedia();
        Result<List<MediaType>> GetMediaTypes();
    }
}
