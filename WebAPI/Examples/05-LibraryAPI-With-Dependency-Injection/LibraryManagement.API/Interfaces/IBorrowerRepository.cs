using LibraryManagement.API.DB;

namespace LibraryManagement.API.Interfaces
{
    public interface IBorrowerRepository
    {
        void Add(Borrower borrower);
        void Update(Borrower borrower);
        List<Borrower> GetAll();
        Borrower? GetById(int id);
        void Delete(Borrower borrower);
    }
}
