using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Tests.Mocks
{
    public class OverLimitCheckoutRepository : ICheckoutRepository
    {
        public void Add(CheckoutLog log)
        {
            throw new NotImplementedException();
        }

        public List<Media> GetAllAvailableMedia()
        {
            throw new NotImplementedException();
        }

        public List<CheckoutLog> GetAllCheckedout()
        {
            throw new NotImplementedException();
        }

        public CheckoutLog GetByID(int checkoutLogID)
        {
            throw new NotImplementedException();
        }

        public bool IsMediaAvailable(int mediaID)
        {
            return true;
        }

        public void Update(CheckoutLog log)
        {
            throw new NotImplementedException();
        }
    }

    public class OverLimitBorrowerRepository : IBorrowerRepository
    {
        public void Add(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public void Delete(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower? GetByEmail(string email)
        {
            var mock = new Borrower();

            mock.BorrowerID = 1;
            mock.FirstName = "Testy";
            mock.LastName = "Tester";
            mock.Email = "tt@test.com";
            mock.Phone = "555555555";

            mock.CheckoutLogs = new List<CheckoutLog>
            {
                new CheckoutLog
                {
                    CheckoutLogID = 1,
                    MediaID = 1,
                    BorrowerID = 1,
                    DueDate = DateTime.Today.AddDays(7),
                    CheckoutDate = DateTime.Today
                },
                new CheckoutLog
                {
                    CheckoutLogID = 2,
                    MediaID = 2,
                    BorrowerID = 1,
                    DueDate = DateTime.Today.AddDays(7),
                    CheckoutDate = DateTime.Today
                },
                new CheckoutLog
                {
                    CheckoutLogID = 3,
                    MediaID = 3,
                    BorrowerID = 1,
                    DueDate = DateTime.Today.AddDays(7),
                    CheckoutDate = DateTime.Today
                }
            };

            return mock;
        }

        public Borrower? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Borrower borrower)
        {
            throw new NotImplementedException();
        }
    }
}
