using LibraryManagement.ConsoleUI.Models;
using System.Text.Json;

namespace LibraryManagement.ConsoleUI.API;

public class CheckoutAPIClient : ICheckoutAPIClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;
    private const string PATH = "api/checkout";

    public CheckoutAPIClient(HttpClient client)
    {
        _httpClient = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<Media>> GetAvailableMediaAsync()
    {
        var response = await _httpClient.GetAsync(PATH);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error getting available media: {content}");
        }

        return JsonSerializer.Deserialize<List<Media>>(content, _options);
    }

    public async Task<List<CheckoutLog>> GetCheckoutLogAsync()
    {
        var response = await _httpClient.GetAsync($"{PATH}/log");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error getting checkout log: {content}");
        }

        return JsonSerializer.Deserialize<List<CheckoutLog>>(content, _options);
    }

    public async Task<bool> CheckoutMediaAsync(int mediaId, string borrowerEmail)
    {
        var response = await _httpClient.PostAsync($"{PATH}/media/{mediaId}/{borrowerEmail}", null);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        var content = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Error checking out media: {content}");
    }

    public async Task<bool> ReturnMediaAsync(int checkoutLogId)
    {
        var response = await _httpClient.PostAsync($"{PATH}/returns/{checkoutLogId}", null);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        var content = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Error returning media: {content}");
    }
}
