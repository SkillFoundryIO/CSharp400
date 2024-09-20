namespace AsyncHttpClient;

public class WebpageGetter
{
    private readonly HttpClient _client;

    public WebpageGetter()
    {
        _client = new HttpClient();
    }

    public async Task<string> FetchDataAsync(string url)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            string statusCode = response.StatusCode.ToString();
            return $"URL: {url} - Status: {statusCode}";
        }
        catch (HttpRequestException e)
        {
            return $"URL: {url} - Error: {e.Message}";
        }
    }
}
