using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private ICheckoutRepository _checkoutRepository;
        private IBorrowerRepository _borrowerRepository;

        public CheckoutService(ICheckoutRepository checkoutRepository, IBorrowerRepository borrowerRepository)
        {
            _checkoutRepository = checkoutRepository;
            _borrowerRepository = borrowerRepository;
        }

        public Result Checkout(int mediaId, string borrowerEmail)
        {
            try
            {
                if(_checkoutRepository.IsMediaAvailable(mediaId))
                {
                    var borrower = _borrowerRepository.GetByEmail(borrowerEmail);

                    if(borrower == null)
                    {
                        return ResultFactory.Fail("Borrower not found!");
                    }
                    else if(borrower.CheckoutLogs.Where(cl=>cl.ReturnDate == null).Count() >= 3)
                    {
                        return ResultFactory.Fail("This borrower has reached their checkout limit!");
                    }
                    else if (borrower.CheckoutLogs.Where(cl => cl.DueDate < DateTime.Today && cl.ReturnDate == null).Any())
                    {
                        return ResultFactory.Fail("This borrower has overdue items and cannot check out more!");
                    }
                    else
                    {
                        var checkoutLog = new CheckoutLog
                        {
                            BorrowerID = borrower.BorrowerID,
                            MediaID = mediaId,
                            CheckoutDate = DateTime.Today,
                            DueDate = DateTime.Today.AddDays(7)
                        };

                        _checkoutRepository.Add(checkoutLog);
                        return ResultFactory.Success();
                    }
                }
                else
                {
                    return ResultFactory.Fail("That item is not available for checkout!");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<CheckoutLog>> GetCheckedoutMedia()
        {
            try
            {
                var logs = _checkoutRepository.GetAllCheckedout();
                return ResultFactory.Success(logs);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLog>>(ex.Message);
            }
        }

        public Result<List<Media>> GetAvailableMedia()
        {
            try
            {
                var media = _checkoutRepository.GetAllAvailableMedia();
                return ResultFactory.Success(media);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result Return(int checkoutLogId)
        {
            try
            {
                var log = _checkoutRepository.GetByID(checkoutLogId);

                if (log.ReturnDate.HasValue)
                {
                    return ResultFactory.Fail<List<CheckoutLog>>("This item is no longer checked out!");
                }

                log.ReturnDate = DateTime.Today;

                _checkoutRepository.Update(log);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLog>>(ex.Message);
            }
        }
    }
}
