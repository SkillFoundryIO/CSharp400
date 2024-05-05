using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories
{
    public class EFMediaRepository : IMediaRepository
    {
        private LibraryContext _dbContext;

        public EFMediaRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Add(Media media)
        {
            _dbContext.Media.Add(media);
            _dbContext.SaveChanges();
        }

        public void Update(Media media)
        {
            _dbContext.Media.Update(media);
            _dbContext.SaveChanges();
        }

        public List<TopMediaItem> GetTopMedia()
        {
            return _dbContext.CheckoutLog
                        .Include(c => c.Media)
                        .GroupBy(c => c.Media)
                        .Select(group => new TopMediaItem
                        {
                            Title = group.Key.Title,
                            CheckoutCount = group.Count()
                        })
                        .ToList();
        }

        public List<Media> GetMediaByType(int mediaTypeID)
        {
            return _dbContext.Media
                        .Include(m => m.MediaType)
                        .Where(m => m.MediaTypeID == mediaTypeID)
                        .OrderBy(m => m.Title)
                        .ToList();
        }

        public List<Media> GetAll()
        {
            return _dbContext.Media
                        .Include(m => m.MediaType)
                        .OrderBy(m => m.Title)
                        .ToList();
        }

        public List<MediaType> GetMediaTypes()
        {
            return _dbContext.MediaType.ToList();
        }
    }
}
