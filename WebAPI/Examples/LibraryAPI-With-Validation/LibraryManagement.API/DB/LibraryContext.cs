using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.DB
{
    public class LibraryContext : DbContext
    {
        public DbSet<Borrower> Borrower { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=LibraryManager;" +
                "User Id=sa;Password=SQLR0ck$;TrustServerCertificate=true;");
        }
    }
}
