using LibraryManagement.ConsoleUI.API;
using LibraryManagement.ConsoleUI.Models;

namespace LibraryManagement.ConsoleUI.IO;

public static class BorrowerWorkflows
{
    public static async Task GetAllBorrowers(IBorrowerAPIClient client)
    {
        Console.Clear();

        try
        {
            var result = await client.GetAllBorrowersAsync();

            Console.WriteLine("Borrower List");
            Console.WriteLine($"{"ID",-5} {"Name",-32} Email");
            Console.WriteLine(new string('=', 70));

            foreach (var b in result)
            {
                Console.WriteLine($"{b.BorrowerID,-5} {b.LastName + ", " + b.FirstName,-32} {b.Email}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task GetBorrower(IBorrowerAPIClient client)
    {
        Console.Clear();

        try
        {
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var result = await client.GetBorrowerAsync(email);

            Console.WriteLine("\nBorrower Information");
            Console.WriteLine("====================");
            Console.WriteLine($"Id: {result.BorrowerID}");
            Console.WriteLine($"Name: {result.LastName}, {result.FirstName}");
            Console.WriteLine($"Email: {result.Email}");
            Console.WriteLine($"Phone: {result.Phone}");


            if (result.CheckoutLogs.Any(c => c.ReturnDate == null))
            {
                Console.WriteLine("\nChecked Out Items");
                Console.WriteLine("=================");

                Console.WriteLine($"{"Title",-40} {"Type",-10} {"Checkout Date",-15} {"Due Date"}");
                foreach (var item in result.CheckoutLogs.Where(c => c.ReturnDate == null))
                {
                    Console.WriteLine($"{item.Media.Title,-40} {item.Media.MediaType.MediaTypeName,-10} {item.CheckoutDate.ToShortDateString(),-15} {item.DueDate.ToShortDateString()}");
                }
            }
            else
            {
                Console.WriteLine("\nNo items checked out.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task AddBorrower(IBorrowerAPIClient client)
    {
        Console.Clear();

        Console.WriteLine("Add New Borrower");
        Console.WriteLine("====================");

        try
        {
            AddBorrowerRequest newBorrower = new();

            newBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
            newBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
            newBorrower.Email = Utilities.GetRequiredString("Email: ");
            newBorrower.Phone = Utilities.GetRequiredString("Phone: ");

            var result = await client.AddBorrowerAsync(newBorrower);

            Console.WriteLine($"Borrower added with id {result.BorrowerID}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task EditBorrower(IBorrowerAPIClient client)
    {
        string email;

        try
        {
            email = Utilities.GetRequiredString("Enter borrower email: ");
            var borrower = await client.GetBorrowerAsync(email);

            EditBorrowerRequest request = new();
            request.BorrowerID = borrower.BorrowerID;
            Console.WriteLine("\nEdit Borrower (press enter to keep original value)");

            request.FirstName = Utilities.GetEditedString($"First Name ({borrower.FirstName}): ", borrower.FirstName);
            request.LastName = Utilities.GetEditedString($"Last Name ({borrower.LastName}): ", borrower.LastName);
            request.Email = Utilities.GetEditedString($"Email ({borrower.Email}): ", borrower.Email);
            request.Phone = Utilities.GetEditedString($"Phone ({borrower.Phone}): ", borrower.Phone);

            await client.EditBorrowerAsync(request);
            Console.WriteLine("Borrower updated!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task DeleteBorrower(IBorrowerAPIClient client)
    {
        string email;

        try
        {
            email = Utilities.GetRequiredString("Enter borrower email: ");
            var borrower = await client.GetBorrowerAsync(email);

            do
            {
                Console.Write($"Are you sure you want to delete {borrower.LastName}, {borrower.FirstName} (Y/N)? ");
                string input = Console.ReadLine().ToUpper();

                if (input == "N")
                {
                    return;
                }
                else if (input == "Y")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter Y or N!");
                    Utilities.AnyKey();
                }
            } while (true);

            await client.DeleteBorrowerAsync(borrower.BorrowerID);
            Console.WriteLine("Borrower deleted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();       
    }
}
