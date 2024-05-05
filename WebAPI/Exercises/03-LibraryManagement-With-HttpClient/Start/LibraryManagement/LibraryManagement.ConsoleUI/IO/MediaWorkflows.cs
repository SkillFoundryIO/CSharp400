using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public class MediaWorkflows
    {
        public static void ListMedia(IMediaService service)
        {
            Console.Clear();

            var mediaTypesResult = service.GetMediaTypes();

            if (mediaTypesResult.Ok)
            {
                int mediaTypeId = GetMediaTypeFromUser(mediaTypesResult.Data);
                var mediaResult = service.ListMedia(mediaTypeId);

                if (mediaResult.Ok)
                {
                    Utilities.PrintMediaList(mediaResult.Data);
                }
                else
                {
                    Console.WriteLine(mediaResult.Message);
                }
            }
            else
            {
                Console.WriteLine(mediaTypesResult.Message);
            }

            Utilities.AnyKey();
        }

        public static void AddMedia(IMediaService service)
        {
            Console.Clear();

            var mediaTypesResult = service.GetMediaTypes();

            if (mediaTypesResult.Ok)
            {
                var newMedia = new Media();
                newMedia.MediaTypeID = GetMediaTypeFromUser(mediaTypesResult.Data);
                newMedia.Title = Utilities.GetRequiredString("Enter title: ");

                var result = service.AddMedia(newMedia);

                if (result.Ok)
                {
                    Console.WriteLine($"Media added with id {newMedia.MediaID}!");
                }
                else
                {
                    Console.WriteLine(result.Message);
                }
            }
            else
            {
                Console.WriteLine(mediaTypesResult.Message);
            }

            Utilities.AnyKey();
        }

        public static void ArchiveMedia(IMediaService service)
        {
            Console.Clear();

            var mediaTypesResult = service.GetMediaTypes();

            if (mediaTypesResult.Ok)
            {
                int mediaTypeID = GetMediaTypeFromUser(mediaTypesResult.Data);
                var mediaResult = service.ListMedia(mediaTypeID);

                if(mediaResult.Ok)
                {
                    var mediaToArchive = Utilities.SelectMediaFromList(mediaResult.Data.Where(m => !m.IsArchived).ToList());
                    mediaToArchive.IsArchived = true;

                    var result = service.EditMedia(mediaToArchive);
                    if(result.Ok)
                    {
                        Console.WriteLine($"{mediaToArchive.Title} is archived.");
                    }
                    else
                    {
                        Console.WriteLine(result.Message);
                    }
                }
                else
                {
                    Console.WriteLine(mediaResult.Message);
                }
            }
            else
            {
                Console.WriteLine(mediaTypesResult.Message);
            }

            Utilities.AnyKey();
        }

        public static void EditMedia(IMediaService service)
        {
            Console.Clear();

            var mediaTypesResult = service.GetMediaTypes();

            if (mediaTypesResult.Ok)
            {
                int mediaTypeID = GetMediaTypeFromUser(mediaTypesResult.Data);
                var mediaResult = service.ListMedia(mediaTypeID);

                if (mediaResult.Ok)
                {
                    var mediaToEdit = Utilities.SelectMediaFromList(mediaResult.Data.Where(m => !m.IsArchived).ToList());
                    mediaToEdit.Title = Utilities.GetEditedString($"Title ({mediaToEdit.Title}): ", mediaToEdit.Title);

                    var result = service.EditMedia(mediaToEdit);
                    if (result.Ok)
                    {
                        Console.WriteLine($"Changes saved!");
                    }
                    else
                    {
                        Console.WriteLine(result.Message);
                    }
                }
                else
                {
                    Console.WriteLine(mediaResult.Message);
                }
            }
            else
            {
                Console.WriteLine(mediaTypesResult.Message);
            }

            Utilities.AnyKey();
        }

        public static void ViewArchive(IMediaService service)
        {
            Console.Clear();
            var result = service.GetArchivedMedia();

            if(result.Ok)
            {
                Utilities.PrintMediaList(result.Data, "Archived Media");
            }
            else
            {
                Console.WriteLine(result.Message);
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

        public static void MostPopularMedia(IMediaService service)
        {
            Console.Clear();

            var result = service.GetMostPopularMedia();

            if(result.Ok)
            {
                Console.WriteLine("Most Popular Media\n");
                Console.WriteLine($"{"Checkout",-9} Title");

                foreach(var item in result.Data)
                {
                    Console.WriteLine($"{item.CheckoutCount,-9} {item.Title}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }
    }
}
