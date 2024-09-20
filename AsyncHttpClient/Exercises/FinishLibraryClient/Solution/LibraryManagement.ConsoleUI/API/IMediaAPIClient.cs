using LibraryManagement.ConsoleUI.Models;

namespace LibraryManagement.ConsoleUI.API;

public interface IMediaAPIClient
{
    Task<List<MediaType>> GetMediaTypesAsync();
    Task<List<Media>> GetMediaByTypeAsync(int mediaTypeId);
    Task<List<TopMediaItem>> GetMostPopularMediaAsync();
    Task<Media> AddMediaAsync(AddMediaRequest media);
    Task EditMediaAsync(Media media);
    Task ArchiveMediaAsync(Media media);
    Task<List<Media>> GetArchivedMediaAsync();
}
