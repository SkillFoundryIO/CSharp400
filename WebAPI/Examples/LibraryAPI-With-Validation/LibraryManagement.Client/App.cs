namespace LibraryManagement.Client
{
    internal class App
    {
        private readonly HttpClient _httpClient;

        public App()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7252")
            };
        }

        public void Run()
        {
            do
            {
                Console.Clear();
                BorrowerMenuChoices choice = Menu.BorrowerMenu();

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
    }
}
