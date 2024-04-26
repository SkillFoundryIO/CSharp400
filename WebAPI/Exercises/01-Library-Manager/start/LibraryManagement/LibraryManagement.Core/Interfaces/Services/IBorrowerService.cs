using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IBorrowerService
    {
        Result<List<Borrower>> GetAllBorrowers();
        Result<Borrower> GetBorrower(string email);
        Result AddBorrower(Borrower newBorrower);
        Result EditBorrower(Borrower editedBorrower);
        Result DeleteBorrower(Borrower deletedBorrower);
    }
}
