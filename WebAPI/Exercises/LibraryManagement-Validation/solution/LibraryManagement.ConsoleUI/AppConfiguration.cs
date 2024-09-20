using LibraryManagement.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.ConsoleUI
{
    public class AppConfiguration : IAppConfiguration
    {
        private IConfiguration _configuration;

        public AppConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .AddUserSecrets<Program>()
                .Build();
        }

        public Uri GetBaseUri()
        {
            return new Uri(_configuration["BaseUri"]);
        }

        public string GetConnectionString()
        {
            return _configuration["LibraryDb"] ?? "";
        }

        public DatabaseMode GetDatabaseMode()
        {
            if (_configuration["DatabaseMode"] == "")
            {
                throw new Exception("DatabaseMode configuration key missing.");
            }

            switch(_configuration["DatabaseMode"])
            {
                case "ORM":
                    return DatabaseMode.ORM;
                case "SQL":
                    return DatabaseMode.DirectSQL;
                default:
                    throw new Exception("DatabaseMode configuration key invalid.");
            }
        }
    }
}
