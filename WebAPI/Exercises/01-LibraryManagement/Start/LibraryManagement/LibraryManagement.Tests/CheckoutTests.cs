using LibraryManagement.Application.Services;
using LibraryManagement.Tests.Mocks;
using NUnit.Framework;

namespace LibraryManagement.Tests
{

    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void FailWithOverdueItems()
        {
            var service = new CheckoutService(new OverdueCheckoutRepository(), new OverdueBorrowerRepository());

            var result = service.Checkout(1, "tt@test.com");

            Assert.That(!result.Ok);
        }

        [Test]
        public void FailBorrowerOverLimit()
        {
            var service = new CheckoutService(new OverLimitCheckoutRepository(), new OverLimitBorrowerRepository());

            var result = service.Checkout(1, "tt@test.com");

            Assert.That(!result.Ok);
        }
    }
}
