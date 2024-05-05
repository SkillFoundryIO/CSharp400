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
    }
}
