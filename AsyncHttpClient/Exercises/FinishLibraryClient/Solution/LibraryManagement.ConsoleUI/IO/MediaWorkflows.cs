using LibraryManagement.ConsoleUI.API;
using LibraryManagement.ConsoleUI.Models;

namespace LibraryManagement.ConsoleUI.IO;

public class MediaWorkflows
{
    public static async Task ListMedia(IMediaAPIClient client)
    {
        Console.Clear();

        try
        {
            var mediaTypes = await client.GetMediaTypesAsync();

            int selectedMediaTypeId = GetMediaTypeFromUser(mediaTypes);
            var media = await client.GetMediaByTypeAsync(selectedMediaTypeId);

            Utilities.PrintMediaList(media);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task AddMedia(IMediaAPIClient client)
    {
        Console.Clear();

        try
        {
            var mediaTypes = await client.GetMediaTypesAsync();

            var newMedia = new AddMediaRequest();
            newMedia.MediaTypeID = GetMediaTypeFromUser(mediaTypes);
            newMedia.Title = Utilities.GetRequiredString("Enter title: ");

            var createdMedia = await client.AddMediaAsync(newMedia);
            Console.WriteLine($"Media added with id {createdMedia.MediaID}!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task ArchiveMedia(IMediaAPIClient client)
    {
        Console.Clear();

        try
        {
            var mediaTypes = await client.GetMediaTypesAsync();
            int selectedMediaTypeId = GetMediaTypeFromUser(mediaTypes);
            var medias = await client.GetMediaByTypeAsync(selectedMediaTypeId);

            var mediaToArchive = Utilities.SelectMediaFromList(medias.Where(m => !m.IsArchived).ToList());
            mediaToArchive.IsArchived = true;
            await client.ArchiveMediaAsync(mediaToArchive);

            Console.WriteLine($"{mediaToArchive.Title} is now archived.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task EditMedia(IMediaAPIClient client)
    {
        Console.Clear();

        try
        {
            var mediaTypes = await client.GetMediaTypesAsync();
            int selectedMediaTypeId = GetMediaTypeFromUser(mediaTypes);
            var medias = await client.GetMediaByTypeAsync(selectedMediaTypeId);

            var mediaToEdit = Utilities.SelectMediaFromList(medias.Where(m => !m.IsArchived).ToList());
            mediaToEdit.Title = Utilities.GetEditedString($"Title ({mediaToEdit.Title}): ", mediaToEdit.Title);

            await client.EditMediaAsync(mediaToEdit);

            Console.WriteLine($"Changes saved!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task ViewArchive(IMediaAPIClient client)
    {
        Console.Clear();

        try
        {
            var medias = await client.GetArchivedMediaAsync();
            Utilities.PrintMediaList(medias, "Archived Media");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    public static async Task MostPopularMedia(IMediaAPIClient client)
    {
        Console.Clear();

        try
        {
            var topMedias = await client.GetMostPopularMediaAsync();

            Console.WriteLine("Most Popular Media\n");
            Console.WriteLine($"{"Checkout",-9} Title");

            foreach (var item in topMedias)
            {
                Console.WriteLine($"{item.CheckoutCount,-9} {item.Title}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API request failed with message: {ex.Message}");
        }

        Utilities.AnyKey();
    }

    private static int GetMediaTypeFromUser(List<MediaType> types)
    {
        Console.WriteLine("Media Types");
        Console.WriteLine("=====================");

        foreach (var type in types)
        {
            Console.WriteLine($"{type.MediaTypeID}. {type.MediaTypeName}");
        }

        return Utilities.GetChoiceInRange(1, types.Count());
    }
}
