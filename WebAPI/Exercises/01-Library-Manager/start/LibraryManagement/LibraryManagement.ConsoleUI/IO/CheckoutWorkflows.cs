using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public class CheckoutWorkflows
    {
        public static void DisplayCheckoutLog(ICheckoutService service)
        {
            Console.Clear();
            Console.WriteLine("Checked Out Media List\n");

            var result = service.GetCheckedoutMedia();

            if(result.Ok)
            {
                if(result.Data.Count() == 0)
                {
                    Console.WriteLine("There are no checked out items.");
                }
                else
                {
                    Console.WriteLine($"{"Title",-40} {"Type",-10} {"Checkout",-10} Due Date");
                    foreach (var log in result.Data)
                    {
                        Console.WriteLine($"{log.Media.Title,-40} {log.Media.MediaType.MediaTypeName,-10} {log.CheckoutDate:d} {log.DueDate:d} {GetOverdueText(log.DueDate)}");
                    }
                }
            
            }

            Utilities.AnyKey();
        }

        public static void CheckoutMedia(ICheckoutService service)
        {
            Console.Clear();
            string borrowerEmail = Utilities.GetRequiredString("Borrower Email: ");

            do
            {
                var mediaResult = service.GetAvailableMedia();

                if (mediaResult.Ok)
                {
                    var selected = Utilities.SelectMediaFromList(mediaResult.Data);

                    var checkoutResult = service.Checkout(selected.MediaID, borrowerEmail);

                    if (checkoutResult.Ok)
                    {
                        Console.WriteLine($"{selected.Title} has been checked out.");
                        if (!Utilities.YesNoPrompt("Check out another item (Y/N)? "))
                        {
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine(checkoutResult.Message);
                        Utilities.AnyKey();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(mediaResult.Message);
                    Utilities.AnyKey();
                    return; // if we can't load the media, we're done.
                }
            } while (true);

        }

        public static void ReturnMedia(ICheckoutService service)
        {
            Console.Clear();
            Console.WriteLine("Select media to return\n");

            var checkoutResult = service.GetCheckedoutMedia();

            if(checkoutResult.Ok)
            {
                if(checkoutResult.Data.Any())
                {
                    var selected = SelectLogFromList(checkoutResult.Data);
                    var returnResult = service.Return(selected.CheckoutLogID);

                    if(returnResult.Ok)
                    {
                        Console.WriteLine($"\n{selected.Media.Title} has been returned.");
                    }
                    else
                    {
                        Console.WriteLine(returnResult.Message);
                    }
                }
                else
                {
                    Console.WriteLine("There are no items checked out.");
                }
            }
            else
            {
                Console.WriteLine(checkoutResult.Message);
            }
            Utilities.AnyKey();
        }

        private static string GetOverdueText(DateTime checkoutDate)
        {
            if(checkoutDate < DateTime.Today)
            {
                return "Overdue";
            }

            return string.Empty;
        }

        private static CheckoutLog SelectLogFromList(List<CheckoutLog> logs)
        {
            Console.WriteLine($"{"ID",-5} {"Title",-40} Due Date");
            foreach(var item in logs)
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
}
