using System.Text.Json;

namespace LibraryManagement.Client
{
    internal class BorrowerWorkflows
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

            if (result.IsSuccessStatusCode)
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

            Borrower newBorrower = new Borrower();

            newBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
            newBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
            newBorrower.Email = Utilities.GetRequiredString("Email: ");
            newBorrower.Phone = Utilities.GetRequiredString("Phone: ");

            var payload = Utilities.ConstructJsonPayload(newBorrower);
            var result = httpClient.PostAsync("/api/borrower/", payload).Result;

            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine($"Borrower created with id: {newBorrower.BorrowerID}");
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
            Borrower? borrower;

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

            Console.WriteLine("\nEdit Borrower (press enter to keep original value)");

            borrower.FirstName = Utilities.GetEditedString($"First Name ({borrower.FirstName}): ", borrower.FirstName);
            borrower.LastName = Utilities.GetEditedString($"Last Name ({borrower.LastName}): ", borrower.LastName);
            borrower.Email = Utilities.GetEditedString($"Email ({borrower.Email}): ", borrower.Email);
            borrower.Phone = Utilities.GetEditedString($"Phone ({borrower.Phone}): ", borrower.Phone);

            var payload = Utilities.ConstructJsonPayload(borrower);
            var updateResult = httpClient.PutAsync("/api/borrower/" + borrower.BorrowerID, payload).Result;

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
