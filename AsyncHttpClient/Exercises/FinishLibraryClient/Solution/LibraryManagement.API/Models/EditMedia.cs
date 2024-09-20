using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Models
{
    public class EditMedia
    {
        public int MediaID { get; set; }
        public int MediaTypeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public bool IsArchived { get; set; }
    }
}
