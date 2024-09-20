using AsyncHttpClient;

List<string> urls = new()
    {
        "https://www.github.com",
        "https://www.yahoo.com",
        "https://www.google.com",
        "https://www.amazon.com"
    };

List<Task<string>> tasks = new List<Task<string>>();

var webpageGetter = new WebpageGetter();

foreach (var url in urls)
{
    tasks.Add(webpageGetter.FetchDataAsync(url));
}

Console.WriteLine("Fetching data...");

while (tasks.Count > 0)
{
    Task<string> completedTask = await Task.WhenAny(tasks);
    tasks.Remove(completedTask);

    string result = await completedTask;
    Console.WriteLine(result);
}

// string[] results = await Task.WhenAll(tasks);

// foreach (var result in results)
// {
//     Console.WriteLine(result);
// }

Console.WriteLine("All requests completed.");

