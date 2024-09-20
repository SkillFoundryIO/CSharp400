namespace LibraryManagement.ConsoleUI.Models;

public class MediaType
{
    public int MediaTypeID { get; set; }
    public string MediaTypeName { get; set; }

    public List<Media> Medias { get; set; }
}
