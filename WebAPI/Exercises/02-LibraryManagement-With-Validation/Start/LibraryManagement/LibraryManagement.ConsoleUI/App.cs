using LibraryManagement.Application;
using LibraryManagement.ConsoleUI.IO;
using LibraryManagement.Core.Interfaces.Application;

namespace LibraryManagement.ConsoleUI
{
    public class App
    {
        IAppConfiguration _config;
        ServiceFactory _serviceFactory;

        public App()
        {
            _config = new AppConfiguration();
            _serviceFactory = new ServiceFactory(_config);
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
                        BorrowerWorkflows.GetAllBorrowers(_serviceFactory.CreateBorrowerService());
                        break;
                    case BorrowerMenuChoices.ViewBorrower:
                        BorrowerWorkflows.GetBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case BorrowerMenuChoices.EditBorrower:
                        BorrowerWorkflows.EditBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case BorrowerMenuChoices.AddBorrower:
                        BorrowerWorkflows.AddBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case BorrowerMenuChoices.DeleteBorrower:
                        BorrowerWorkflows.DeleteBorrower(_serviceFactory.CreateBorrowerService());
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
                        MediaWorkflows.ListMedia(_serviceFactory.CreateMediaService());
                        break;
                    case MediaMenuChoices.AddMedia:
                        MediaWorkflows.AddMedia(_serviceFactory.CreateMediaService());
                        break;
                    case MediaMenuChoices.EditMedia:
                        MediaWorkflows.EditMedia(_serviceFactory.CreateMediaService());
                        break;
                    case MediaMenuChoices.ArchiveMedia:
                        MediaWorkflows.ArchiveMedia(_serviceFactory.CreateMediaService());
                        break;
                    case MediaMenuChoices.ViewArchive:
                        MediaWorkflows.ViewArchive(_serviceFactory.CreateMediaService());
                        break;
                    case MediaMenuChoices.MostPopularMediaReport:
                        MediaWorkflows.MostPopularMedia(_serviceFactory.CreateMediaService());
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
                        CheckoutWorkflows.DisplayCheckoutLog(_serviceFactory.CreateCheckoutService());
                        break;
                    case CheckoutMenuChoices.Checkout:
                        CheckoutWorkflows.CheckoutMedia(_serviceFactory.CreateCheckoutService());
                        break;
                    case CheckoutMenuChoices.Return:
                        CheckoutWorkflows.ReturnMedia(_serviceFactory.CreateCheckoutService());
                        break;
                    case CheckoutMenuChoices.GoBack:
                        return;
                }
            } while (true);
        }
    }
}
