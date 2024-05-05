using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class BorrowerService : IBorrowerService
    {
        private IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public Result AddBorrower(Borrower newBorrower)
        {
            try
            {
                var duplicate = _borrowerRepository.GetByEmail(newBorrower.Email);
                if (duplicate != null)
                {
                    return ResultFactory.Fail($"Borrower with email: {newBorrower.Email} already exists!");
                }

                _borrowerRepository.Add(newBorrower);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result DeleteBorrower(Borrower deletedBorrower)
        {
            try
            {
                _borrowerRepository.Delete(deletedBorrower);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditBorrower(Borrower editedBorrower)
        {
            try
            {
                var duplicate = _borrowerRepository.GetByEmail(editedBorrower.Email);
                if (duplicate != null && duplicate.BorrowerID != editedBorrower.BorrowerID) 
                {
                    return ResultFactory.Fail($"Borrower with email: {editedBorrower.Email} already exists!");
                }

                _borrowerRepository.Update(editedBorrower);
                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Borrower>> GetAllBorrowers()
        {
            try
            {
                var borrowers = _borrowerRepository.GetAll();
                return ResultFactory.Success(borrowers);
               
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Borrower>>(ex.Message);
            }
        }

        public Result<Borrower> GetBorrower(string email)
        {
            try
            {
                var borrower = _borrowerRepository.GetByEmail(email);

                return borrower is null ? 
                    ResultFactory.Fail<Borrower>($"Borrower with email:{email} not found!") : 
                    ResultFactory.Success(borrower);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Borrower>(ex.Message);
            }
        }
    }
}
