using LibraryManagement.ConsoleUI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace LibraryManagement.ConsoleUI.API
{
    public class BorrowerAPIClient : IBorrowerAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH="api/borrower";

        public BorrowerAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<Borrower> AddBorrowerAsync(AddBorrowerRequest borrower)
        {
            var response = await _httpClient.PostAsJsonAsync(PATH, borrower);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error adding borrower: {content}");
            }
           
            return JsonSerializer.Deserialize<Borrower>(content, _options);
        }

        public async Task DeleteBorrowerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{PATH}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error deleting borrower: {content}");
            }
        }

        public async Task EditBorrowerAsync(EditBorrowerRequest borrower)
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/{borrower.BorrowerID}", borrower);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error editing borrower: {content}");
            }
        }

        public async Task<List<Borrower>> GetAllBorrowersAsync()
        {
            var response = await _httpClient.GetAsync(PATH);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting borrowers: {content}");
            }
            
            return JsonSerializer.Deserialize<List<Borrower>>(content, _options);
        }

        public async Task<Borrower> GetBorrowerAsync(string email)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{email}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error retrieving borrower: {content}");
            }

            return JsonSerializer.Deserialize<Borrower>(content, _options);
        }
    }
}
