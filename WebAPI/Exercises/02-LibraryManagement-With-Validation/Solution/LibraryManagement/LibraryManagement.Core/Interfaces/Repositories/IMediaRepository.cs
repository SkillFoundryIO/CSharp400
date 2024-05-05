using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IMediaRepository
    {
        List<Media> GetMediaByType(int mediaTypeID);
        List<Media> GetAll();
        void Add(Media media);
        void Update(Media media);
        List<TopMediaItem> GetTopMedia();
        List<MediaType> GetMediaTypes();
    }
}
