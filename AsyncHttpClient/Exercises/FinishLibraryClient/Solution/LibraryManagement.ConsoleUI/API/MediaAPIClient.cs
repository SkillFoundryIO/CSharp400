using LibraryManagement.ConsoleUI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace LibraryManagement.ConsoleUI.API;

public class MediaAPIClient : IMediaAPIClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;
    private const string PATH = "api/media";

    public MediaAPIClient(HttpClient client)
    {
        _httpClient = client;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<MediaType>> GetMediaTypesAsync()
    {
        var response = await _httpClient.GetAsync($"{PATH}/types");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error getting media types: {content}");
        }

        return JsonSerializer.Deserialize<List<MediaType>>(content, _options);
    }

    public async Task<List<Media>> GetMediaByTypeAsync(int mediaTypeId)
    {
        var response = await _httpClient.GetAsync($"{PATH}/types/{mediaTypeId}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error getting media by type: {content}");
        }

        return JsonSerializer.Deserialize<List<Media>>(content, _options);
    }

    public async Task<List<TopMediaItem>> GetMostPopularMediaAsync()
    {
        var response = await _httpClient.GetAsync($"{PATH}/top");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error getting most popular media: {content}");
        }

        return JsonSerializer.Deserialize<List<TopMediaItem>>(content, _options);
    }

    public async Task<Media> AddMediaAsync(AddMediaRequest media)
    {
        var response = await _httpClient.PostAsJsonAsync(PATH, media);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error adding media: {content}");
        }

        return JsonSerializer.Deserialize<Media>(content, _options);
    }

    public async Task EditMediaAsync(Media media)
    {
        var response = await _httpClient.PutAsJsonAsync($"{PATH}/{media.MediaID}", media);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error editing media: {content}");
        }
    }

    public async Task ArchiveMediaAsync(Media media)
    {
        var response = await _httpClient.PostAsJsonAsync($"{PATH}/{media.MediaID}/archive", media);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error archiving media: {content}");
        }
    }

    public async Task<List<Media>> GetArchivedMediaAsync()
    {
        var response = await _httpClient.GetAsync($"{PATH}/archived");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error getting archived media: {content}");
        }

        return JsonSerializer.Deserialize<List<Media>>(content, _options);
    }
}
