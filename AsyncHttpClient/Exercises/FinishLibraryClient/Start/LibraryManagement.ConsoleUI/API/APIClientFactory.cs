namespace LibraryManagement.ConsoleUI.API;

public static class APIClientFactory
{
    public static IBorrowerAPIClient GetBorrowerClient(HttpClient client)
    {
        return new BorrowerAPIClient(client);
    }
}
