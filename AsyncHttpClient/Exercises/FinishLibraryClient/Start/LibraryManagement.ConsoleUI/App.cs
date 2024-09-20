using LibraryManagement.ConsoleUI.API;
using LibraryManagement.ConsoleUI.IO;

namespace LibraryManagement.ConsoleUI;

public class App
{
    private IAppConfiguration _config;
    private HttpClient _httpClient;

    private IBorrowerAPIClient _borrowerAPI;

    public App()
    {
        _config = new AppConfiguration();
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(_config.GetBaseUrl())
        };

        _borrowerAPI = APIClientFactory.GetBorrowerClient(_httpClient);
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
                    //MediaWorkflows.ListMedia(_serviceFactory.CreateMediaService());
                    break;
                case MediaMenuChoices.AddMedia:
                    //MediaWorkflows.AddMedia(_serviceFactory.CreateMediaService());
                    break;
                case MediaMenuChoices.EditMedia:
                    //MediaWorkflows.EditMedia(_serviceFactory.CreateMediaService());
                    break;
                case MediaMenuChoices.ArchiveMedia:
                    //MediaWorkflows.ArchiveMedia(_serviceFactory.CreateMediaService());
                    break;
                case MediaMenuChoices.ViewArchive:
                    //MediaWorkflows.ViewArchive(_serviceFactory.CreateMediaService());
                    break;
                case MediaMenuChoices.MostPopularMediaReport:
                    //MediaWorkflows.MostPopularMedia(_serviceFactory.CreateMediaService());
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
                    //CheckoutWorkflows.DisplayCheckoutLog(_serviceFactory.CreateCheckoutService());
                    break;
                case CheckoutMenuChoices.Checkout:
                    //CheckoutWorkflows.CheckoutMedia(_serviceFactory.CreateCheckoutService());
                    break;
                case CheckoutMenuChoices.Return:
                    //CheckoutWorkflows.ReturnMedia(_serviceFactory.CreateCheckoutService());
                    break;
                case CheckoutMenuChoices.GoBack:
                    return;
            }
        } while (true);
    }
}
