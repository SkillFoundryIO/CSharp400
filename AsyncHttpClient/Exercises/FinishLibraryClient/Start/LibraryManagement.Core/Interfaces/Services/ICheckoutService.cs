using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface ICheckoutService
    {
        Result<List<CheckoutLog>> GetCheckedoutMedia();
        Result Checkout(int mediaId, string borrowerEmail);
        Result Return(int checkoutLogId);
        Result<List<Media>> GetAvailableMedia();
    }
}
