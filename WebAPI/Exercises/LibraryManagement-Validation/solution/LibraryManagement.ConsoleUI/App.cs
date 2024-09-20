using LibraryManagement.ConsoleUI.IO;
using LibraryManagement.Core.Interfaces.Application;

namespace LibraryManagement.ConsoleUI
{
    public class App
    {
        IAppConfiguration _config;
        HttpClient _httpClient;

        public App()
        {
            _config = new AppConfiguration();
            _httpClient = new HttpClient()
            {
                BaseAddress = _config.GetBaseUri()
            };
        }

        public void Run()
        {
            do
            {
                Console.Clear();
                MainMenuChoices choice = Menus.MainMenu();

                switch (choice)
                {
                    case MainMenuChoices.BorrowerManagement:
                        ShowBorrowerManagementMenu();
                        break;
                    case MainMenuChoices.MediaManagement:
                        ShowMediaManagementMenu();
                        break;
                    case MainMenuChoices.CheckoutManagement:
                        ShowCheckoutManagementMenu();
                        break;
                    case MainMenuChoices.Quit:
                        return;
                }
            } while (true);
        }

        private void ShowBorrowerManagementMenu()
        {
            do
            {
                Console.Clear();
                BorrowerMenuChoices choice = Menus.BorrowerMenu();

                switch (choice)
                {
                    case BorrowerMenuChoices.ListAllBorrowers:
                        BorrowerWorkflows.GetAllBorrowers(_httpClient);
                        break;
                    case BorrowerMenuChoices.ViewBorrower:
                        BorrowerWorkflows.GetBorrower(_httpClient);
                        break;
                    case BorrowerMenuChoices.EditBorrower:
                        BorrowerWorkflows.EditBorrower(_httpClient);
                        break;
                    case BorrowerMenuChoices.AddBorrower:
                        BorrowerWorkflows.AddBorrower(_httpClient);
                        break;
                    case BorrowerMenuChoices.DeleteBorrower:
                        BorrowerWorkflows.DeleteBorrower(_httpClient);
                        break;
                    case BorrowerMenuChoices.GoBack:
                        return;
                }
            } while (true);         
        }

        private void ShowMediaManagementMenu()
        {
            do
            {
                Console.Clear();
                MediaMenuChoices choice = Menus.MediaMenu();

                switch (choice)
                {
                    case MediaMenuChoices.ListMedia:
                        MediaWorkflows.ListMedia(_httpClient);
                        break;
                    case MediaMenuChoices.AddMedia:
                        MediaWorkflows.AddMedia(_httpClient);
                        break;
                    case MediaMenuChoices.EditMedia:
                        MediaWorkflows.EditMedia(_httpClient);
                        break;
                    case MediaMenuChoices.ArchiveMedia:
                        MediaWorkflows.ArchiveMedia(_httpClient);
                        break;
                    case MediaMenuChoices.ViewArchive:
                        MediaWorkflows.ViewArchive(_httpClient);
                        break;
                    case MediaMenuChoices.MostPopularMediaReport:
                        MediaWorkflows.MostPopularMedia(_httpClient);
                        break;
                    case MediaMenuChoices.GoBack:
                        return;
                }
            } while (true);
        }

        private void ShowCheckoutManagementMenu()
        {
            do
            {
                Console.Clear();
                CheckoutMenuChoices choice = Menus.CheckoutMenu();

                switch (choice)
                {
                    case CheckoutMenuChoices.CheckoutLog:
                        CheckoutWorkflows.DisplayCheckoutLog(_httpClient);
                        break;
                    case CheckoutMenuChoices.Checkout:
                        CheckoutWorkflows.CheckoutMedia(_httpClient);
                        break;
                    case CheckoutMenuChoices.Return:
                        CheckoutWorkflows.ReturnMedia(_httpClient);
                        break;
                    case CheckoutMenuChoices.GoBack:
                        return;
                }
            } while (true);
        }
    }
}
