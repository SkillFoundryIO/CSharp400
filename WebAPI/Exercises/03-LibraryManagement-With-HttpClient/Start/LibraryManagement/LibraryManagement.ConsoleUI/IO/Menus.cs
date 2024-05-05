namespace LibraryManagement.ConsoleUI.IO
{
    public static class Menus
    {
        public static MainMenuChoices MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Library Manager Main Menu");
            Console.WriteLine("=========================");
            Console.WriteLine("1. Borrower Management");
            Console.WriteLine("2. Media Management");
            Console.WriteLine("3. Checkout Management");
            Console.WriteLine("4. Quit");

            return (MainMenuChoices)Utilities.GetChoiceInRange(1, 4);
        }

        public static BorrowerMenuChoices BorrowerMenu()
        {
            Console.WriteLine("Borrower Management");
            Console.WriteLine("===================");
            Console.WriteLine("1. List all Borrowers");
            Console.WriteLine("2. View a Borrower");
            Console.WriteLine("3. Edit a Borrower");
            Console.WriteLine("4. Add a Borrower");
            Console.WriteLine("5. Delete a Borrower");
            Console.WriteLine("6. Go Back");

            return (BorrowerMenuChoices)Utilities.GetChoiceInRange(1, 6);
        }

        public static MediaMenuChoices MediaMenu()
        {
            Console.WriteLine("Media Management");
            Console.WriteLine("================");
            Console.WriteLine("1. List Media");
            Console.WriteLine("2. Add Media");
            Console.WriteLine("3. Edit Media");
            Console.WriteLine("4. Archive Media");
            Console.WriteLine("5. View Archive");
            Console.WriteLine("6. Most Popular Media Report");
            Console.WriteLine("7. Go Back");

            return (MediaMenuChoices)Utilities.GetChoiceInRange(1, 7);
        }

        public static CheckoutMenuChoices CheckoutMenu()
        {
            Console.WriteLine("Checkout Management");
            Console.WriteLine("===================");
            Console.WriteLine("1. Checkout");
            Console.WriteLine("2. Return");
            Console.WriteLine("3. Checkout Log");
            Console.WriteLine("4. Go Back");

            return (CheckoutMenuChoices)Utilities.GetChoiceInRange(1, 4);
        }


    }

    public enum MainMenuChoices
    {
        BorrowerManagement = 1,
        MediaManagement,
        CheckoutManagement,
        Quit
    }

    public enum BorrowerMenuChoices
    {
        ListAllBorrowers = 1,
        ViewBorrower,
        EditBorrower,
        AddBorrower,
        DeleteBorrower,
        GoBack
    }

    public enum MediaMenuChoices
    {
        ListMedia = 1,
        AddMedia,
        EditMedia,
        ArchiveMedia,
        ViewArchive,
        MostPopularMediaReport,
        GoBack
    }

    public enum CheckoutMenuChoices
    {
        Checkout = 1,
        Return,
        CheckoutLog,
        GoBack
    }
}
