using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LibraryManagement.Data.Repositories.Dapper
{
    public class DapperCheckoutRepository : ICheckoutRepository
    {
        private string _connectionString;

        public DapperCheckoutRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(CheckoutLog log)
        {
            var sql = @"INSERT INTO CheckoutLog (MediaID, BorrowerID, CheckoutDate, DueDate)
                        VALUES (@MediaID, @BorrowerID, @CheckoutDate, @DueDate);
                        SELECT SCOPE_IDENTITY();";

            using(var cn = new SqlConnection(_connectionString))
            {
                var p = new
                {
                    log.MediaID,
                    log.BorrowerID,
                    log.CheckoutDate,
                    log.DueDate
                };

                log.CheckoutLogID = cn.ExecuteScalar<int>(sql, p);
            }
        }

        public List<Media> GetAllAvailableMedia()
        {
            var sql = @"SELECT * 
                        FROM Media 
                        WHERE IsArchived=0 AND MediaID NOT IN
                        (SELECT MediaID FROM CheckoutLog WHERE ReturnDate IS NULL)";

            using(var cn = new SqlConnection(_connectionString))
            {
                return cn.Query<Media>(sql).ToList();
            }
        }

        public List<CheckoutLog> GetAllCheckedout()
        {
            List<CheckoutLog> logs = new List<CheckoutLog>();

            var sql = @"SELECT cl.CheckoutLogID, cl.MediaID, cl.BorrowerID, cl.CheckoutDate, cl.DueDate, 
                        b.Email, b.FirstName, b.LastName, m.Title, m.MediaTypeID, mt.MediaTypeName
                        FROM CheckoutLog cl 
                            INNER JOIN Media m ON cl.MediaID = m.MediaID 
                            INNER JOIN Borrower b ON cl.BorrowerID = b.BorrowerID 
                            INNER JOIN MediaType mt ON m.MediaTypeID = mt.MediaTypeID
                        WHERE ReturnDate IS NULL";

            using(var cn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sql, cn);

                cn.Open();

                using(var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var log = new CheckoutLog();
                        log.Borrower = new Borrower();
                        log.Media = new Media();
                        log.Media.MediaType = new MediaType();

                        log.CheckoutLogID = (int)dr["CheckoutLogID"];
                        log.BorrowerID = log.Borrower.BorrowerID = (int)dr["BorrowerID"];
                        log.MediaID = log.Media.MediaID = (int)dr["MediaID"];
                        log.Media.MediaTypeID = log.Media.MediaType.MediaTypeID = (int)dr["MediaTypeID"];
                        log.CheckoutDate = (DateTime)dr["CheckoutDate"];
                        log.DueDate = (DateTime)dr["DueDate"];
                        log.Media.Title = (string)dr["Title"];
                        log.Borrower.FirstName = (string)dr["FirstName"];
                        log.Borrower.LastName = (string)dr["LastName"];
                        log.Borrower.Email = (string)dr["Email"];
                        log.Media.MediaType.MediaTypeName = (string)dr["MediaTypeName"];

                        logs.Add(log);                       
                    }
                }

                return logs;
            }
        }

        public CheckoutLog GetByID(int checkoutLogID)
        {
            CheckoutLog log = null;

            var sql = @"SELECT cl.CheckoutLogID, cl.MediaID, cl.BorrowerID, cl.CheckoutDate, cl.DueDate, 
                        b.Email, b.FirstName, b.LastName, m.Title, m.MediaTypeID, mt.MediaTypeName
                        FROM CheckoutLog cl 
                            INNER JOIN Media m ON cl.MediaID = m.MediaID 
                            INNER JOIN Borrower b ON cl.BorrowerID = b.BorrowerID 
                            INNER JOIN MediaType mt ON m.MediaTypeID = mt.MediaTypeID
                        WHERE ReturnDate IS NULL AND CheckoutLogID = @CheckoutLogID";

            

            using (var cn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@CheckoutLogID", checkoutLogID);

                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        log = new CheckoutLog();
                        log.Borrower = new Borrower();
                        log.Media = new Media();
                        log.Media.MediaType = new MediaType();

                        log.CheckoutLogID = (int)dr["CheckoutLogID"];
                        log.BorrowerID = log.Borrower.BorrowerID = (int)dr["BorrowerID"];
                        log.MediaID = log.Media.MediaID = (int)dr["MediaID"];
                        log.Media.MediaTypeID = log.Media.MediaType.MediaTypeID = (int)dr["MediaTypeID"];
                        log.CheckoutDate = (DateTime)dr["CheckoutDate"];
                        log.DueDate = (DateTime)dr["DueDate"];
                        log.Media.Title = (string)dr["Title"];
                        log.Borrower.FirstName = (string)dr["FirstName"];
                        log.Borrower.LastName = (string)dr["LastName"];
                        log.Borrower.Email = (string)dr["Email"];
                        log.Media.MediaType.MediaTypeName = (string)dr["MediaTypeName"];
                    }
                }

                return log;
            }

        }

        public bool IsMediaAvailable(int mediaID)
        {
            var sql = @"SELECT Count(*)
                        FROM CheckoutLog 
                        WHERE MediaID = @MediaID AND ReturnDate IS NULL;";

            var p = new
            {
                MediaID = mediaID
            };

            using(var cn = new SqlConnection(_connectionString))
            {
                return cn.ExecuteScalar<int>(sql, p) == 0;
            }
        }

        public void Update(CheckoutLog log)
        {
            var sql = @"UPDATE CheckoutLog SET 
                            MediaID = @MediaID, 
                            BorrowerID = @BorrowerID, 
                            CheckoutDate = @CheckoutDate,
                            DueDate = @DueDate,
                            ReturnDate = @ReturnDate
                        WHERE CheckoutLogID = @CheckoutLogID";

            using (var cn = new SqlConnection(_connectionString))
            {
                var p = new
                {
                    log.CheckoutLogID,
                    log.MediaID,
                    log.BorrowerID,
                    log.CheckoutDate,
                    log.DueDate,
                    log.ReturnDate
                };

                cn.Execute(sql, p);
            }
        }
    }
}
