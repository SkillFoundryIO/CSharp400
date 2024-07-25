using LibraryManagement.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.MVC.Models
{
    public class CheckoutModel
    {
        public Media? Media { get; set; }
        [Required]
        public string? BorrowerEmail { get; set; }
    }
}
