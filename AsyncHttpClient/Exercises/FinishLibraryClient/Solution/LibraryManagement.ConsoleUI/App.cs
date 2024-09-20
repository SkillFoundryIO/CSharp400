using LibraryManagement.ConsoleUI.API;
using LibraryManagement.ConsoleUI.IO;

namespace LibraryManagement.ConsoleUI;

public class App
{
    private IAppConfiguration _config;
    private HttpClient _httpClient;

    private IBorrowerAPIClient _borrowerAPI;
    private IMediaAPIClient _mediaAPI;
    private ICheckoutAPIClient _checkoutAPI;

    public App()
    {
        _config = new AppConfiguration();
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(_config.GetBaseUrl())
        };

        _borrowerAPI = APIClientFactory.GetBorrowerClient(_httpClient);
        _mediaAPI = APIClientFactory.GetMediaClient(_httpClient);
        _checkoutAPI = APIClientFactory.GetCheckoutClient(_httpClient);
    }

    public async Task Run()
    {
        do
        {
            Console.Clear();
            MainMenuChoices choice = Menus.MainMenu();

            switch (choice)
            {
                case MainMenuChoices.BorrowerManagement:
                    await ShowBorrowerManagementMenu();
                    break;
                case MainMenuChoices.MediaManagement:
                    await ShowMediaManagementMenu();
                    break;
                case MainMenuChoices.CheckoutManagement:
                    await ShowCheckoutManagementMenu();
                    break;
                case MainMenuChoices.Quit:
                    return;
            }
        } while (true);
    }

    private async Task ShowBorrowerManagementMenu()
    {
        do
        {
            Console.Clear();
            BorrowerMenuChoices choice = Menus.BorrowerMenu();

            switch (choice)
            {
                case BorrowerMenuChoices.ListAllBorrowers:
                    await BorrowerWorkflows.GetAllBorrowers(_borrowerAPI);
                    break;
                case BorrowerMenuChoices.ViewBorrower:
                    await BorrowerWorkflows.GetBorrower(_borrowerAPI);
                    break;
                case BorrowerMenuChoices.EditBorrower:
                    await BorrowerWorkflows.EditBorrower(_borrowerAPI);
                    break;
                case BorrowerMenuChoices.AddBorrower:
                    await BorrowerWorkflows.AddBorrower(_borrowerAPI);
                    break;
                case BorrowerMenuChoices.DeleteBorrower:
                    await BorrowerWorkflows.DeleteBorrower(_borrowerAPI);
                    break;
                case BorrowerMenuChoices.GoBack:
                    return;
            }
        } while (true);         
    }

    private async Task ShowMediaManagementMenu()
    {
        do
        {
            Console.Clear();
            MediaMenuChoices choice = Menus.MediaMenu();

            switch (choice)
            {
                case MediaMenuChoices.ListMedia:
                    await MediaWorkflows.ListMedia(_mediaAPI);
                    break;
                case MediaMenuChoices.AddMedia:
                    await MediaWorkflows.AddMedia(_mediaAPI);
                    break;
                case MediaMenuChoices.EditMedia:
                    await MediaWorkflows.EditMedia(_mediaAPI);
                    break;
                case MediaMenuChoices.ArchiveMedia:
                    await MediaWorkflows.ArchiveMedia(_mediaAPI);
                    break;
                case MediaMenuChoices.ViewArchive:
                    await MediaWorkflows.ViewArchive(_mediaAPI);
                    break;
                case MediaMenuChoices.MostPopularMediaReport:
                    await MediaWorkflows.MostPopularMedia(_mediaAPI);
                    break;
                case MediaMenuChoices.GoBack:
                    return;
            }
        } while (true);
    }

    private async Task ShowCheckoutManagementMenu()
    {
        do
        {
            Console.Clear();
            CheckoutMenuChoices choice = Menus.CheckoutMenu();

            switch (choice)
            {
                case CheckoutMenuChoices.CheckoutLog:
                    await CheckoutWorkflows.DisplayCheckoutLog(_checkoutAPI);
                    break;
                case CheckoutMenuChoices.Checkout:
                    await CheckoutWorkflows.CheckoutMedia(_checkoutAPI);
                    break;
                case CheckoutMenuChoices.Return:
                    await CheckoutWorkflows.ReturnMedia(_checkoutAPI);
                    break;
                case CheckoutMenuChoices.GoBack:
                    return;
            }
        } while (true);
    }
}
