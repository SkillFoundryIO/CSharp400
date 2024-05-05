namespace LibraryManagement.Client
{
    internal class Menu
    {
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
    }
}
