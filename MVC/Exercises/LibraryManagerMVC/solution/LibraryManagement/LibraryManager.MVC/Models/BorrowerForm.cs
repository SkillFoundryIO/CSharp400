using LibraryManagement.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.MVC.Models
{
    public class BorrowerForm
    {
        public int? BorrowerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public BorrowerForm()
        {

        }

        public BorrowerForm(Borrower entity)
        {
            BorrowerID = entity.BorrowerID;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
            Phone = entity.Phone;
        }

        public Borrower ToEntity()
        {
            return new Borrower()
            {
                BorrowerID = BorrowerID ?? 0,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone
            };
        }
    }
}
