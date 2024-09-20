using LibraryManagement.ConsoleUI.Models;

namespace LibraryManagement.ConsoleUI.API;

public interface IBorrowerAPIClient
{
    Task<List<Borrower>> GetAllBorrowersAsync();
    Task<Borrower> GetBorrowerAsync(string email);
    Task<Borrower> AddBorrowerAsync(AddBorrowerRequest borrower);
    Task EditBorrowerAsync(EditBorrowerRequest borrower);
    Task DeleteBorrowerAsync(int id);
}

