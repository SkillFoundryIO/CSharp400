using LibraryManagement.Application.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Tests.Mocks;
using NUnit.Framework;

namespace LibraryManagement.Tests
{
    [TestFixture]
    public class BorrowerTests
    {
        [Test]
        public void DuplicateEmailCannotAdd()
        {
            BorrowerService service = new BorrowerService(new DuplicateBorrowerRepository());

            Borrower toAdd = new Borrower
            {
                FirstName = "Test",
                LastName = "Borrower",
                Email = "test@example.com",
                Phone = "55555555555"
            };

            var result = service.AddBorrower(toAdd);

            Assert.That(result.Ok, Is.False);
        }
    }
}
