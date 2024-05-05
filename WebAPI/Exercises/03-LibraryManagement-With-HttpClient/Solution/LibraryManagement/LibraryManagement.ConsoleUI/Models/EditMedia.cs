namespace LibraryManagement.ConsoleUI.Models
{
    public class EditMedia
    {
        public int MediaID { get; set; }
        public int MediaTypeID { get; set; }
        public string Title { get; set; }
        public bool IsArchived { get; set; }
    }
}
