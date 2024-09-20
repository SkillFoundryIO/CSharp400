using LibraryManagement.ConsoleUI.API;
using LibraryManagement.ConsoleUI.Models;

namespace LibraryManagement.ConsoleUI.IO;

public class CheckoutWorkflows
{
    public static async Task DisplayCheckoutLog(ICheckoutAPIClient client)
    {
        Console.Clear();

        try
        {
            var logs = await client.GetCheckoutLogAsync();

            Console.WriteLine("Checked Out Media List\n");

            if (!logs.Any())
            {
                Console.WriteLine("There are no checked out items.");
            }
            else
            {
                Console.WriteLine($"{"Title",-40} {"Type",-15} {"Checkout",-10} Due Date");
                foreach (var log in logs)
                {
                    Console.WriteLine($"{log.Media.Title,-40} {log.Media.MediaType.MediaTypeName,-15} {log.CheckoutDate:d} {log.DueDate:d} {GetOverdueText(log.DueDate)}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task CheckoutMedia(ICheckoutAPIClient client)
    {
        Console.Clear();
        string borrowerEmail = Utilities.GetRequiredString("Borrower Email: ");

        try
        {
            do
            {
                var availableMedia = await client.GetAvailableMediaAsync();

                var selectedMedia = Utilities.SelectMediaFromList(availableMedia);

                if (await client.CheckoutMediaAsync(selectedMedia.MediaID, borrowerEmail))
                {
                    Console.WriteLine($"{selectedMedia.Title} has been checked out.");
                    if (!Utilities.YesNoPrompt("Check out another item (Y/N)? "))
                    {
                        return;
                    }
                }
            } while (true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }
    }

    public static async Task ReturnMedia(ICheckoutAPIClient client)
    {
        Console.Clear();

        try
        {
            var checkedOutMedia = await client.GetCheckoutLogAsync();

            if (!checkedOutMedia.Any())
            {
                Console.WriteLine("There are no items checked out.");
            }
            else
            {
                Console.WriteLine("Select media to return\n");
                var selectedMedia = SelectLogFromList(checkedOutMedia);

                await client.ReturnMediaAsync(selectedMedia.CheckoutLogID);
                Console.WriteLine($"\n{selectedMedia.Media.Title} has been returned.");
            }         
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    private static string GetOverdueText(DateTime checkoutDate)
    {
        if (checkoutDate < DateTime.Today)
        {
            return "Overdue";
        }

        return string.Empty;
    }

    private static CheckoutLog SelectLogFromList(List<CheckoutLog> logs)
    {
        Console.WriteLine($"{"ID",-5} {"Title",-40} Due Date");
        foreach (var item in logs)
        {
            Console.WriteLine($"{item.CheckoutLogID,-5} {item.Media.Title,-40} {item.DueDate:d}");
        }

        int id;

        do
        {
            Console.Write("Enter ID: ");

            if (int.TryParse(Console.ReadLine(), out id))
            {
                var selected = logs.FirstOrDefault(c => c.CheckoutLogID == id);

                if (selected != null)
                {
                    return selected;
                }
            }

            Console.WriteLine("That is not a valid id!");
        } while (true);
    }
}
