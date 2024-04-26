using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.DB
{
    public class Borrower
    {
        public int BorrowerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class LibraryContext : DbContext
    {
        public DbSet<Borrower> Borrower { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=127.0.0.1,1433;Database=LibraryManager;" +
                "User Id=sa;Password=SQLR0ck$;TrustServerCertificate=true;");
        }
    }
}
