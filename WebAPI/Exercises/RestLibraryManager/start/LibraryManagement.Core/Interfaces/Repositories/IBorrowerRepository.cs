using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IBorrowerRepository
    {
        void Add(Borrower borrower);
        void Update(Borrower borrower);
        List<Borrower> GetAll();
        Borrower? GetById(int id);
        Borrower? GetByEmail(string email);
        void Delete(Borrower borrower);
    }
}
