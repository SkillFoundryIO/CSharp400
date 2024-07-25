using LibraryManagement.Core.Entities;

namespace LibraryManagement.MVC.Models
{
    public class BorrowerList
    {
        public string? SearchEmail { get; set; }
        public IEnumerable<Borrower>? Borrowers { get; set; }
    }
}
