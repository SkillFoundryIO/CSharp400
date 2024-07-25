namespace LibraryManagement.Core.Entities
{
    public class CheckoutLog
    {
        public int CheckoutLogID { get; set; }
        public int MediaID { get; set; }
        public int BorrowerID { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Borrower Borrower { get; set; }
        public Media Media { get; set; }
    }
}
