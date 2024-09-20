using LibraryManagement.Core.Entities;
using System.Text;
using System.Text.Json;

namespace LibraryManagement.ConsoleUI.IO
{
    public class MediaWorkflows
    {
        public static void ListMedia(HttpClient httpClient)
        {
            Console.Clear();

            var mediaTypesResult = httpClient.GetAsync("/api/media/types").Result;

            if (mediaTypesResult.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<MediaType>>(
                    Utilities.GetStringContentFromResponse(mediaTypesResult),
                    Utilities.GetJsonSerializerOptions());

                int mediaTypeId = GetMediaTypeFromUser(data);

                var mediaResult = httpClient.GetAsync("/api/media/types/" + mediaTypeId).Result;

                if (mediaResult.IsSuccessStatusCode)
                {
                    var mediaData = JsonSerializer.Deserialize<List<Media>>(
                        Utilities.GetStringContentFromResponse(mediaResult),
                        Utilities.GetJsonSerializerOptions());

                    Utilities.PrintMediaList(mediaData);
                }
                else
                {
                    Console.WriteLine(Utilities.GetStringContentFromResponse(mediaResult));
                }
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(mediaTypesResult));
            }

            Utilities.AnyKey();
        }

        public static void AddMedia(HttpClient httpClient)
        {
            Console.Clear();

            var mediaTypesResult = httpClient.GetAsync("/api/media/types").Result;

            if (mediaTypesResult.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<MediaType>>(
                    Utilities.GetStringContentFromResponse(mediaTypesResult),
                    Utilities.GetJsonSerializerOptions());

                var newMedia = new Media();
                newMedia.MediaTypeID = GetMediaTypeFromUser(data);
                newMedia.Title = Utilities.GetRequiredString("Enter title: ");

                var payload = Utilities.ConstructJsonPayload(newMedia);
                var result = httpClient.PostAsync("/api/media", payload).Result;

                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Media added with id {newMedia.MediaID}!");
                }
                else
                {
                    Console.WriteLine(Utilities.GetStringContentFromResponse(result));
                }
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(mediaTypesResult));
            }

            Utilities.AnyKey();
        }

        public static void ArchiveMedia(HttpClient httpClient)
        {
            Console.Clear();

            var mediaTypesResult = httpClient.GetAsync("/api/media/types").Result;

            if (mediaTypesResult.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<MediaType>>(
                    Utilities.GetStringContentFromResponse(mediaTypesResult),
                    Utilities.GetJsonSerializerOptions());

                int mediaTypeId = GetMediaTypeFromUser(data);
                var mediaResult = httpClient.GetAsync("/api/media/types/" + mediaTypeId).Result;

                if (mediaResult.IsSuccessStatusCode)
                {
                    var mediaData = JsonSerializer.Deserialize<List<Media>>(
                        Utilities.GetStringContentFromResponse(mediaResult),
                        Utilities.GetJsonSerializerOptions());

                    var mediaToArchive = Utilities.SelectMediaFromList(mediaData.Where(m => !m.IsArchived).ToList());
                    mediaToArchive.IsArchived = true;

                    var payload = Utilities.ConstructJsonPayload(mediaToArchive);
                    var result = httpClient.PostAsync("/api/media/" + mediaToArchive.MediaID + "/archive", payload).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{mediaToArchive.Title} is archived.");
                    }
                    else
                    {
                        Console.WriteLine(Utilities.GetStringContentFromResponse(result));
                    }
                }
                else
                {
                    Console.WriteLine(Utilities.GetStringContentFromResponse(mediaResult));
                }
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(mediaTypesResult));
            }

            Utilities.AnyKey();
        }

        public static void EditMedia(HttpClient httpClient)
        {
            Console.Clear();

            var mediaTypesResult = httpClient.GetAsync("/api/media/types").Result;

            if (mediaTypesResult.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<MediaType>>(
                    Utilities.GetStringContentFromResponse(mediaTypesResult),
                    Utilities.GetJsonSerializerOptions());

                int mediaTypeId = GetMediaTypeFromUser(data);
                var mediaResult = httpClient.GetAsync("/api/media/types/" + mediaTypeId).Result;

                if (mediaResult.IsSuccessStatusCode)
                {
                    var mediaData = JsonSerializer.Deserialize<List<Media>>(
                        Utilities.GetStringContentFromResponse(mediaResult),
                        Utilities.GetJsonSerializerOptions());

                    var mediaToEdit = Utilities.SelectMediaFromList(mediaData.Where(m => !m.IsArchived).ToList());
                    mediaToEdit.Title = Utilities.GetEditedString($"Title ({mediaToEdit.Title}): ", mediaToEdit.Title);

                    var payload = Utilities.ConstructJsonPayload(mediaToEdit);
                    var result = httpClient.PutAsync("/api/media/" + mediaToEdit.MediaID, payload).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Changes saved!");
                    }
                    else
                    {
                        Console.WriteLine(Utilities.GetStringContentFromResponse(result));
                    }
                }
                else
                {
                    Console.WriteLine(Utilities.GetStringContentFromResponse(mediaResult));
                }
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(mediaTypesResult));
            }

            Utilities.AnyKey();
        }

        public static void ViewArchive(HttpClient httpClient)
        {
            Console.Clear();
            var result = httpClient.GetAsync("/api/media/archived").Result;

            if (result.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<Media>>(
                    Utilities.GetStringContentFromResponse(result),
                    Utilities.GetJsonSerializerOptions());
                Utilities.PrintMediaList(data, "Archived Media");
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(result));
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

        public static void MostPopularMedia(HttpClient httpClient)
        {
            Console.Clear();

            var result = httpClient.GetAsync("/api/media/top").Result;

            if (result.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<List<TopMediaItem>>(
                    Utilities.GetStringContentFromResponse(result),
                    Utilities.GetJsonSerializerOptions());

                Console.WriteLine("Most Popular Media\n");
                Console.WriteLine($"{"Checkout",-9} Title");

                foreach(var item in data)
                {
                    Console.WriteLine($"{item.CheckoutCount,-9} {item.Title}");
                }
            }
            else
            {
                Console.WriteLine(Utilities.GetStringContentFromResponse(result));
            }

            Utilities.AnyKey();
        }
    }
}
