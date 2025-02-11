namespace LibraryManagement.API.Models;

public class CheckoutLogDTO
{
    public int CheckoutLogID { get; set; }
    public string BorrowerName { get; set; }
    public string Title { get; set; }
    public string MediaTypeName { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
}