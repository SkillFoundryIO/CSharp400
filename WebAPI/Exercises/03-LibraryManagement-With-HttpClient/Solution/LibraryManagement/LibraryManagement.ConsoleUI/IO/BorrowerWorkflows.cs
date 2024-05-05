using LibraryManagement.ConsoleUI.Models;
using LibraryManagement.Core.Entities;
using System.Text.Json;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class BorrowerWorkflows
    {
        public static void GetAllBorrowers(HttpClient httpClient)
        {
            Console.Clear();
            Console.WriteLine("Borrower List");
            Console.WriteLine($"{"ID",-5} {"Name",-32} Email");
            Console.WriteLine(new string('=', 70));
            var result = httpClient.GetAsync("/api/borrower").Result;

            if (result.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<Borrower>>(
                    Utilities.GetStringContentFromResponse(result),
                    Utilities.GetJsonSerializerOptions());

                foreach (var b in data)
                {
                    Console.WriteLine($"{b.BorrowerID,-5} {b.LastName + ", " + b.FirstName,-32} {b.Email}");
                }
            }
            else
            {

                Console.WriteLine(Utilities.GetStringContentFromResponse(result));
            }

            Utilities.AnyKey();
        }

        public static void GetBorrower(HttpClient httpClient)
        {
            Console.Clear();
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var result = httpClient.GetAsync("/api/borrower/" + email).Result;

            if (result.IsSuccessStatusCode )
            {
                var data = JsonSerializer.Deserialize<Borrower>(
                    Utilities.GetStringContentFromResponse(result),
                    Utilities.GetJsonSerializerOptions());

                Console.WriteLine("\nBorrower Information");
                Console.WriteLine("====================");
                Console.WriteLine($"Id: {data.BorrowerID}");
                Console.WriteLine($"Name: {data.LastName}, {data.FirstName}");
                Console.WriteLine($"Email: {data.Email}");
                Console.WriteLine($"Phone: {data.Phone}");


                if (data.CheckoutLogs.Any(c => c.ReturnDate == null))
                {
                    Console.WriteLine("\nChecked Out Items");
                    Console.WriteLine("=================");

                    Console.WriteLine($"{"Title",-40} {"Type",-10} {"Checkout Date",-15} {"Due Date"}");
                    foreach(var item in data.CheckoutLogs.Where(c=>c.ReturnDate == null))
                    {
                        Console.WriteLine($"{item.Media.Title,-40} {item.Media.MediaType.MediaTypeName,-10} {item.CheckoutDate.ToShortDateString(),-15} {item.DueDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo items checked out.");
                }
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(result));
            }

            Utilities.AnyKey();
        }

        public static void AddBorrower(HttpClient httpClient)
        {
            Console.Clear();
            Console.WriteLine("Add New Borrower");
            Console.WriteLine("====================");

            AddBorrower newBorrower = new AddBorrower();

            newBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
            newBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
            newBorrower.Email = Utilities.GetRequiredString("Email: ");
            newBorrower.Phone = Utilities.GetRequiredString("Phone: ");

            var payload = Utilities.ConstructJsonPayload(newBorrower);
            var result = httpClient.PostAsync("/api/borrower/", payload).Result;

            if (result.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<Borrower>(
                    Utilities.GetStringContentFromResponse(result),
                    Utilities.GetJsonSerializerOptions());
                Console.WriteLine($"Borrower created with id: {data.BorrowerID}");
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(result));
            }

            Utilities.AnyKey();
        }

        public static void EditBorrower(HttpClient httpClient)
        {
            string email;
            Borrower? originalBorrower;

            do
            {
                email = Utilities.GetRequiredString("Enter borrower email: ");

                var borrowerResult = httpClient.GetAsync("/api/borrower/" + email).Result;

                if (borrowerResult.IsSuccessStatusCode)
                {
                    originalBorrower = JsonSerializer.Deserialize<Borrower>(
                        Utilities.GetStringContentFromResponse(borrowerResult),
                        Utilities.GetJsonSerializerOptions());
                    break;
                }
                Console.WriteLine("Could not find borrower with that email!");
            } while (true);

            Console.WriteLine("\nEdit Borrower (press enter to keep original value)");

            var editedBorrower = new EditBorrower();

            editedBorrower.BorrowerID = originalBorrower.BorrowerID;
            editedBorrower.FirstName = Utilities.GetEditedString($"First Name ({originalBorrower.FirstName}): ", originalBorrower.FirstName);
            editedBorrower.LastName = Utilities.GetEditedString($"Last Name ({originalBorrower.LastName}): ", originalBorrower.LastName);
            editedBorrower.Email = Utilities.GetEditedString($"Email ({originalBorrower.Email}): ", originalBorrower.Email);
            editedBorrower.Phone = Utilities.GetEditedString($"Phone ({originalBorrower.Phone}): ", originalBorrower.Phone);

            var payload = Utilities.ConstructJsonPayload(editedBorrower);
            var updateResult = httpClient.PutAsync("/api/borrower/" + editedBorrower.BorrowerID, payload).Result;

            if (updateResult.IsSuccessStatusCode)
            {
                Console.WriteLine("Borrower updated!");
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(updateResult));
            }

            Utilities.AnyKey();
        }

        public static void DeleteBorrower(HttpClient httpClient)
        {
            string email;
            Borrower borrower;

            do
            {
                email = Utilities.GetRequiredString("Enter borrower email: ");
                var borrowerResult = httpClient.GetAsync("/api/borrower/" + email).Result;

                if (borrowerResult.IsSuccessStatusCode)
                {
                    borrower = JsonSerializer.Deserialize<Borrower>(
                        Utilities.GetStringContentFromResponse(borrowerResult),
                        Utilities.GetJsonSerializerOptions());
                    break;
                }
                Console.WriteLine("Could not find borrower with that email!");
            } while (true);

            do
            {
                Console.Write($"Are you sure you want to delete {borrower.LastName}, {borrower.FirstName} (Y/N)? ");
                string input = Console.ReadLine().ToUpper();

                if(input == "N")
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

            var updateResult = httpClient.DeleteAsync("/api/borrower/" + borrower.BorrowerID).Result;

            if (updateResult.IsSuccessStatusCode)
            {
                Console.WriteLine("Borrower deleted!");
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(updateResult));
            }

            Utilities.AnyKey();
        }
    }
}
