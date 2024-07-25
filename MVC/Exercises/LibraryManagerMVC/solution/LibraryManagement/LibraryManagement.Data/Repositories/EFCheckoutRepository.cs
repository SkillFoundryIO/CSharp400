﻿
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private LibraryContext _dbContext;

        public EFCheckoutRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Add(CheckoutLog log)
        {
            _dbContext.CheckoutLog.Add(log);
            _dbContext.SaveChanges();
        }

        public void Update(CheckoutLog log)
        {
            _dbContext.CheckoutLog.Update(log);
            _dbContext.SaveChanges();
        }

        public List<Media> GetAllAvailableMedia()
        {
            var checkedOutMediaIDs = _dbContext.CheckoutLog
                .Where(cl => cl.ReturnDate == null)
                .Select(cl => cl.MediaID);

            return _dbContext.Media
                .Include(m => m.MediaType)
                .Where(m => !m.IsArchived && !checkedOutMediaIDs.Contains(m.MediaID))
                .OrderBy(m => m.MediaType.MediaTypeName)
                    .ThenBy(m => m.Title)
                .ToList();
        }

        public List<CheckoutLog> GetAllCheckedout()
        {
            return _dbContext.CheckoutLog
                        .Include(cl => cl.Borrower)
                        .Include(cl => cl.Media)
                            .ThenInclude(m => m.MediaType)
                        .Where(cl => cl.ReturnDate == null)
                        .OrderBy(cl => cl.DueDate)
                        .ToList();
        }

        public CheckoutLog GetByID(int checkoutLogID)
        {
            return _dbContext.CheckoutLog
                        .Include(cl => cl.Borrower)
                        .Include(cl => cl.Media)
                            .ThenInclude(m => m.MediaType)
                        .Where(cl => cl.CheckoutLogID == checkoutLogID)
                        .Single();

        }

        public bool IsMediaAvailable(int mediaID)
        {
            return !_dbContext.CheckoutLog
                       .Where(cl => cl.MediaID == mediaID && cl.ReturnDate == null)
                       .Any();
        }
    }
}
