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

        public string GetBaseUrl()
        {
            if (_configuration["BaseUrl"] == "")
            {
                throw new Exception("BaseUrl configuration key missing.");
            }

            return _configuration["BaseUrl"];
        }
    }
}
