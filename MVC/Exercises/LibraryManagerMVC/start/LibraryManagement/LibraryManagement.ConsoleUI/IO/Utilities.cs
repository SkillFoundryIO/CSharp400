
using LibraryManagement.Core.Entities;

namespace LibraryManagement.ConsoleUI.IO
{
    public class Utilities
    {
        public static void AnyKey()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static int GetPositiveInteger(string prompt)
        {
            int result;

            do
            {
                Console.Write(prompt);
                if(int.TryParse(Console.ReadLine(), out result))
                {
                    if(result > 0)
                    {
                        return result;
                    }
                }

                Console.WriteLine("Invalid input, must be a positive integer!");
                AnyKey();
            } while (true);
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

        public static Media SelectMediaFromList(List<Media> mediaList)
        {
            PrintMediaList(mediaList);

            int id;

            do
            {
                Console.Write("Enter ID: ");
                
                if(int.TryParse(Console.ReadLine(), out id))
                {
                    var media = mediaList.FirstOrDefault(m => m.MediaID == id);

                    if(media != null)
                    {
                        return media;
                    }
                }

                Console.WriteLine("That is not a valid id!");
            } while (true);
        }

        public static void PrintMediaList(List<Media> mediaList, string header = "Media List")
        {
            Console.Clear();
            Console.WriteLine($"{header}\n");

            Console.WriteLine($"{"ID",-3} {"Title",-100} Status");

            foreach (var media in mediaList)
            {
                Console.WriteLine($"{media.MediaID,-3} {media.Title,-100} {GetArchivedText(media.IsArchived)}");
            }
        }

        public static string GetArchivedText(bool archived)
        {
            return archived ? "Archived" : "Available";
        }

        public static bool YesNoPrompt(string prompt)
        {
            do
            {
                Console.Write(prompt);

                if(Console.ReadLine().ToUpper() == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } while (true);
        }
    }
}
