using LibraryManagement.Core.Entities;
using System.Text;
using System.Text.Json;

namespace LibraryManagement.ConsoleUI.IO
{
    public class CheckoutWorkflows
    {
        public static void DisplayCheckoutLog(HttpClient httpClient)
        {
            Console.Clear();
            Console.WriteLine("Checked Out Media List\n");

            var result = httpClient.GetAsync("/api/checkout/log").Result;

            if (result.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<CheckoutLog>>(
                    Utilities.GetStringContentFromResponse(result),
                    Utilities.GetJsonSerializerOptions());

                if (data.Count() == 0)
                {
                    Console.WriteLine("There are no checked out items.");
                }
                else
                {
                    Console.WriteLine($"{"Title",-40} {"Type",-10} {"Checkout",-10} Due Date");
                    foreach (var log in data)
                    {
                        Console.WriteLine($"{log.Media.Title,-40} {log.Media.MediaType.MediaTypeName,-10} {log.CheckoutDate:d} {log.DueDate:d} {GetOverdueText(log.DueDate)}");
                    }
                }
            
            }

            Utilities.AnyKey();
        }

        public static void CheckoutMedia(HttpClient httpClient)
        {
            Console.Clear();
            string borrowerEmail = Utilities.GetRequiredString("Borrower Email: ");

            do
            {
                var mediaResult = httpClient.GetAsync("/api/checkout").Result;

                if (mediaResult.IsSuccessStatusCode)
                {
                    var checkedOutMedia = JsonSerializer.Deserialize<List<Media>>(
                        Utilities.GetStringContentFromResponse(mediaResult),
                        Utilities.GetJsonSerializerOptions());

                    var selectedMedia = Utilities.SelectMediaFromList(checkedOutMedia);

                    var checkoutResult = httpClient.PostAsync($"/api/checkout/media/{selectedMedia.MediaID}/{borrowerEmail}", null).Result;

                    if (checkoutResult.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{selectedMedia.Title} has been checked out.");
                        if (!Utilities.YesNoPrompt("Check out another item (Y/N)? "))
                        {
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine(Utilities.GetStringContentFromResponse(checkoutResult));
                        Utilities.AnyKey();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine(Utilities.GetStringContentFromResponse(mediaResult));
                    Utilities.AnyKey();
                    return; // if we can't load the media, we're done.
                }
            } while (true);

        }

        public static void ReturnMedia(HttpClient httpClient)
        {
            Console.Clear();
            Console.WriteLine("Select media to return\n");

            var checkoutResult = httpClient.GetAsync("/api/checkout/log").Result;

            if (checkoutResult.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<CheckoutLog>>(
                    Utilities.GetStringContentFromResponse(checkoutResult),
                    Utilities.GetJsonSerializerOptions());

                if (data.Any())
                {
                    var selected = SelectLogFromList(data);
                    var payload = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                    var returnResult = httpClient.PostAsync("/api/checkout/returns/" + selected.CheckoutLogID, payload).Result;

                    if (returnResult.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"\n{selected.Media.Title} has been returned.");
                    }
                    else
                    {
                        Console.WriteLine(Utilities.GetStringContentFromResponse(returnResult));
                    }
                }
                else
                {
                    Console.WriteLine("There are no items checked out.");
                }
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(checkoutResult));
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
