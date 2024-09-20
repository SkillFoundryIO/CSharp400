using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface ICheckoutRepository
    {
        void Add(CheckoutLog log);
        void Update(CheckoutLog log);
        List<Media> GetAllAvailableMedia();
        List<CheckoutLog> GetAllCheckedout();
        CheckoutLog GetByID(int checkoutLogID);
        bool IsMediaAvailable(int mediaID);
    }
}
