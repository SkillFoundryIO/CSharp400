using System.Text.Json;
using System.Text;

namespace LibraryManagement.Client
{
    internal class Utilities
    {
        public static void AnyKey()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static string GetRequiredString(string prompt)
        {
            string? input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Input is required.");
                AnyKey();
            } while (true);
        }

        public static string GetEditedString(string prompt, string originalValue)
        {
            string? input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    return originalValue;
                }
                else
                {
                    return input;
                }
            } while (true);
        }

        public static int GetChoiceInRange(int low, int high)
        {
            int choice;

            do
            {
                Console.Write($"\nEnter your choice ({low}-{high}): ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= low && choice <= high)
                    {
                        return choice;
                    }
                }

                Console.WriteLine("Invalid choice!");
            } while (true);
        }

        public static string GetStringContentFromResponse(HttpResponseMessage result)
        {
            return result.Content.ReadAsStringAsync().Result;
        }

        public static StringContent ConstructJsonPayload(object data)
        {
            var payload = JsonSerializer.Serialize(data);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }

        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
    }
}
