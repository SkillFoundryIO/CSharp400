using LibraryManagement.ConsoleUI;

await RunAsync();

async Task RunAsync()
{
    var app = new App();
    await app.Run();
}
