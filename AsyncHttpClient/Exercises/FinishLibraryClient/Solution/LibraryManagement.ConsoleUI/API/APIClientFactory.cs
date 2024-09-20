
namespace LibraryManagement.ConsoleUI.API;

public static class APIClientFactory
{
    public static IBorrowerAPIClient GetBorrowerClient(HttpClient client)
    {
        return new BorrowerAPIClient(client);
    }

    public static IMediaAPIClient GetMediaClient(HttpClient client)
    {
        return new MediaAPIClient(client);
    }

    public static ICheckoutAPIClient GetCheckoutClient(HttpClient client)
    {
        return new CheckoutAPIClient(client);
    }
}
