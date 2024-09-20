using LibraryManagement.ConsoleUI.Models;

namespace LibraryManagement.ConsoleUI.API;

public interface ICheckoutAPIClient
{
    Task<List<Media>> GetAvailableMediaAsync();
    Task<List<CheckoutLog>> GetCheckoutLogAsync();
    Task<bool> CheckoutMediaAsync(int mediaId, string borrowerEmail);
    Task<bool> ReturnMediaAsync(int checkoutLogId);
}
