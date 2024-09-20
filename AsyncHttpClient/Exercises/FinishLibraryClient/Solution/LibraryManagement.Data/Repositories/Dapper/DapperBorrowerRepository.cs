using Dapper;
using Dapper.Transaction;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Data.Repositories.Dapper
{
    public class DapperBorrowerRepository : IBorrowerRepository
    {
        private string _connectionString;

        public DapperBorrowerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Borrower borrower)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Borrower (FirstName, LastName, Email, Phone) " +
                    "VALUES (@FirstName, @LastName, @Email, @Phone); " +
                    "SELECT SCOPE_IDENTITY();";

                var p = new
                {
                    borrower.FirstName,
                    borrower.LastName,
                    borrower.Email,
                    borrower.Phone
                };

                borrower.BorrowerID = cn.ExecuteScalar<int>(sql, p);
            }
        }

        public void Delete(Borrower borrower)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql1 = "DELETE FROM CheckoutLog WHERE BorrowerID = @BorrowerID;";
                var sql2 = "DELETE FROM Borrower WHERE BorrowerID = @BorrowerID";

                var p = new
                {
                    borrower.BorrowerID
                };

                cn.Open();

                using (var transaction = cn.BeginTransaction())
                {
                    transaction.Execute(sql1, p);
                    transaction.Execute(sql2, p);
                    transaction.Commit();
                }
            }
        }

        public List<Borrower> GetAll()
        {
            using(var cn = new SqlConnection(_connectionString))
            {
                return cn.Query<Borrower>("SELECT * FROM Borrower;").ToList();
            }
        }

        public Borrower? GetByEmail(string email)
        {
            Borrower? borrower;

            using (var cn = new SqlConnection(_connectionString))
            {
                
                var sql = @"SELECT * FROM Borrower WHERE Email = @Email;

                            SELECT CheckoutLogID, cl.BorrowerID, cl.MediaID, CheckoutDate, 
                                DueDate, ReturnDate, mt.MediaTypeID, MediaTypeName, Title
                            FROM CheckoutLog cl
                                INNER JOIN Media m ON cl.MediaID = m.MediaID
                                INNER JOIN MediaType mt ON m.MediaTypeID = mt.MediaTypeID
                                INNER JOIN Borrower b ON cl.BorrowerID = b.BorrowerID
                            WHERE Email = @Email AND ReturnDate IS NULL;";

                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Email", email);

                cn.Open();

                using(var dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        borrower = new Borrower();
                        borrower.BorrowerID = (int)dr["BorrowerID"];
                        borrower.FirstName = (string)dr["FirstName"];
                        borrower.LastName = (string)dr["LastName"];
                        borrower.Email = (string)dr["Email"];
                        borrower.Phone = (string)dr["Phone"];

                        dr.NextResult();
                        borrower.CheckoutLogs = new List<CheckoutLog>();

                        while (dr.Read())
                        {
                            var log = new CheckoutLog();
                            log.Media = new Media();
                            log.Media.MediaType = new MediaType();

                            log.BorrowerID = (int)dr["BorrowerID"];
                            log.CheckoutLogID = (int)dr["CheckoutLogID"];
                            log.MediaID = log.Media.MediaID = (int)dr["MediaID"];
                            log.DueDate = (DateTime)dr["DueDate"];
                            log.CheckoutDate = (DateTime)dr["CheckoutDate"];
                            log.ReturnDate = dr["ReturnDate"] == DBNull.Value ? null : (DateTime)dr["CheckoutDate"];
                            log.Media.Title = (string)dr["Title"];
                            log.Media.MediaTypeID = log.Media.MediaType.MediaTypeID = (int)dr["MediaTypeID"];
                            log.Media.MediaType.MediaTypeName = (string)dr["MediaTypeName"];

                            borrower.CheckoutLogs.Add(log);
                        }
                    }  
                    else
                    {
                        return null;
                    }
                }
            }

            return borrower;
        }

        public Borrower? GetById(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Borrower WHERE BorrowerID = @BorrowerID";
                var p = new { BorrowerID = id };
                return cn.Query<Borrower>(sql, p).FirstOrDefault();
            }
        }

        public void Update(Borrower borrower)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Borrower SET 
                                FirstName = @FirstName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                Phone = @Phone
                            WHERE BorrowerID = @BorrowerID";

                var p = new
                {
                    borrower.BorrowerID,
                    borrower.FirstName,
                    borrower.LastName,
                    borrower.Email,
                    borrower.Phone
                };

                cn.Execute(sql, p);
            }
        }
    }
}
