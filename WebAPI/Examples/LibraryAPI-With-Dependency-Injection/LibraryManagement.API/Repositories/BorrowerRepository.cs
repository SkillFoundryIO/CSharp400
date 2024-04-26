using LibraryManagement.API.DB;
using LibraryManagement.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Repositories
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly LibraryContext _dbContext;

        public BorrowerRepository(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Borrower borrower)
        {
            _dbContext.Borrower.Add(borrower);
            _dbContext.SaveChanges();
        }

        public void Delete(Borrower borrower)
        {
            _dbContext.Borrower
                .Where(b => b.BorrowerID == borrower.BorrowerID)
                .ExecuteDelete();
        }

        public List<Borrower> GetAll()
        {
            return _dbContext.Borrower.ToList();
        }

        public Borrower? GetById(int id)
        {
            return _dbContext.Borrower
                .FirstOrDefault(b => b.BorrowerID == id);
        }

        public void Update(Borrower borrower)
        {
            _dbContext.Borrower.Update(borrower);
            _dbContext.SaveChanges();
        }
    }
}
