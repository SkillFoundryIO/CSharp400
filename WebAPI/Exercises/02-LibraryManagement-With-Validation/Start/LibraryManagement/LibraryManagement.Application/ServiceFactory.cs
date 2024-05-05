using LibraryManagement.Application.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Application;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Data.Repositories;
using LibraryManagement.Data.Repositories.Dapper;

namespace LibraryManagement.Application
{
    public class ServiceFactory
    {
        private IAppConfiguration _config;

        public ServiceFactory(IAppConfiguration config)
        {
            _config = config;
        }

        public IBorrowerService CreateBorrowerService()
        {
            if(_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new BorrowerService(
                    new EFBorrowerRepository(_config.GetConnectionString()));
            }
            else
            {
                return new BorrowerService(
                    new DapperBorrowerRepository(_config.GetConnectionString()));
            }
        }

        public IMediaService CreateMediaService()
        {
            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new MediaService(
                    new EFMediaRepository(_config.GetConnectionString()));
            }
            else
            {
                return new MediaService(
                    new DapperMediaRepository(_config.GetConnectionString()));
            }
        }

        public ICheckoutService CreateCheckoutService()
        {
            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new CheckoutService(
                    new EFCheckoutRepository(_config.GetConnectionString()),
                    new EFBorrowerRepository(_config.GetConnectionString()));
            }
            else
            {
                return new CheckoutService(
                    new DapperCheckoutRepository(_config.GetConnectionString()),
                    new DapperBorrowerRepository(_config.GetConnectionString()));
            }
        }
    }
}
