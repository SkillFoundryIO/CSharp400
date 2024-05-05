using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LibraryManagement.Data.Repositories.Dapper
{
    public class DapperMediaRepository : IMediaRepository
    {
        private string _connectionString;

        public DapperMediaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Media media)
        {
            var sql = @"INSERT INTO Media (Title, MediaTypeID)
                        VALUES (@Title, @MediaTypeID);
                        SELECT SCOPE_IDENTITY();";

            var p = new
            {
                media.Title,
                media.MediaTypeID
            };

            var cn = new SqlConnection(_connectionString);
            media.MediaID = cn.ExecuteScalar<int>(sql, p);
        }

        public List<Media> GetAll()
        {
            var medias = new List<Media>();
            var sql = @"SELECT MediaID, Title, IsArchived, mt.MediaTypeID, MediaTypeName 
                        FROM Media m 
                            INNER JOIN MediaType mt ON m.MediaTypeID = mt.MediaTypeID
                        ORDER BY Title;";

            using (var cn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sql, cn);

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var media = new Media();
                        media.MediaType = new MediaType();

                        media.MediaID = (int)dr["MediaID"];
                        media.MediaTypeID = media.MediaType.MediaTypeID = (int)dr["MediaTypeID"];
                        media.Title = (string)dr["Title"];
                        media.IsArchived = (bool)dr["IsArchived"];

                        medias.Add(media);
                    }
                }
            };

            return medias;
        }

        public List<Media> GetMediaByType(int mediaTypeID)
        {
            var medias = new List<Media>();
            var sql = @"SELECT MediaID, Title, IsArchived, mt.MediaTypeID, MediaTypeName 
                        FROM Media m 
                            INNER JOIN MediaType mt ON m.MediaTypeID = mt.MediaTypeID
                        WHERE m.MediaTypeID = @MediaTypeID
                        ORDER BY Title;";

            using (var cn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@MediaTypeID", mediaTypeID);

                cn.Open();
                using(var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        var media = new Media();
                        media.MediaType = new MediaType();

                        media.MediaID = (int)dr["MediaID"];
                        media.MediaTypeID = media.MediaType.MediaTypeID = (int)dr["MediaTypeID"];
                        media.Title = (string)dr["Title"];
                        media.IsArchived = (bool)dr["IsArchived"];

                        medias.Add(media);
                    }
                }
            };

            return medias;
        }

        public List<MediaType> GetMediaTypes()
        {
            var sql = "SELECT MediaTypeID, MediaTypeName FROM MediaType;";

            using (var cn = new SqlConnection(_connectionString))
            {
                return cn.Query<MediaType>(sql).ToList();
            };
           
        }

        public List<TopMediaItem> GetTopMedia()
        {
            var sql = @"SELECT TOP 3 cl.MediaID, Title, Count(cl.CheckoutLogID) as CheckoutCount
                        FROM CheckoutLog cl 
                            INNER JOIN Media m ON cl.MediaID = m.MediaID 
                        GROUP BY cl.MediaID, Title
                        ORDER BY CheckoutCount DESC";

            using(var cn = new SqlConnection(_connectionString))
            {
                return cn.Query<TopMediaItem>(sql).ToList();
            }
        }

        public void Update(Media media)
        {
            var sql = @"UPDATE Media 
                        SET Title = @Title, 
                            IsArchived = @IsArchived,
                            MediaTypeID = @MediaTypeID
                        WHERE MediaID = @MediaID";

            var p = new
            {
                media.MediaID,
                media.Title,
                media.MediaTypeID,
                media.IsArchived
            };

            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Execute(sql, p);
            };
        }
    }
}
