using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Models
{
    public class AddMedia
    {
        public int MediaTypeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
}
