using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories
{
    public class EFBorrowerRepository : IBorrowerRepository
    {
        private LibraryContext _dbContext;

        public EFBorrowerRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Add(Borrower borrower)
        {
            _dbContext.Borrower.Add(borrower);
            _dbContext.SaveChanges();
        }

        public void Delete(Borrower borrower)
        {
            using(var transaction = _dbContext.Database.BeginTransaction())
            {
                _dbContext.CheckoutLog
                    .Where(c => c.BorrowerID == borrower.BorrowerID)
                    .ExecuteDelete();

                _dbContext.Borrower
                    .Where(b => b.BorrowerID == borrower.BorrowerID)
                    .ExecuteDelete();

                transaction.Commit();
            }
        }

        public List<Borrower> GetAll()
        {
            return _dbContext.Borrower.ToList();
        }

        public Borrower? GetByEmail(string email)
        {
            return _dbContext.Borrower
                .Include(b => b.CheckoutLogs)

                    .ThenInclude(c => c.Media)
                    .ThenInclude(m => m.MediaType)
                .FirstOrDefault(b => b.Email == email);
        }

        public Borrower? GetById(int id)
        {
            return _dbContext.Borrower
                .Include(b => b.CheckoutLogs)
                    .ThenInclude(c => c.Media)
                    .ThenInclude(m => m.MediaType)
                .FirstOrDefault(b => b.BorrowerID == id);
        }

        public void Update(Borrower borrower)
        {
            _dbContext.Borrower.Update(borrower);
            _dbContext.SaveChanges();
        }
    }
}
