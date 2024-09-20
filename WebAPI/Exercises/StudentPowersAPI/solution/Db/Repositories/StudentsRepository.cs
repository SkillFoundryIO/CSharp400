using Dapper;
using StudentPowersAPI.Interfaces;
using System.Data.SqlClient;

namespace StudentPowersAPI.Db.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly string _connectionString;

        public StudentsRepository()
        {
            // 127.0.0.1 is the localhost IP address
            _connectionString = "Server=127.0.0.1,1433;Database=FourthWallAcademy;User Id=SA;Password=SQLR0ck$";
        }

        public List<Student> GetAllStudents()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<Student>("SELECT StudentID, FirstName, LastName, Alias, DoB FROM Student")
                    .ToList();
            }
        }

        public void AddStudent(Student student)
        {
            var sql = @"INSERT INTO Student (FirstName, LastName, Alias, DoB) 
                        VALUES (@FirstName, @LastName, @Alias, @DoB); 

                        SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(_connectionString))
            {
                student.StudentID = connection.ExecuteScalar<int>(sql, new
                {
                    student.FirstName,
                    student.LastName,
                    student.Alias,
                    student.DoB
                });
            }
        }

        public StudentDetail? GetStudentById(int studentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var student = connection
                    .QuerySingleOrDefault<StudentDetail?>(
                        @"SELECT
                            StudentID,
                            FirstName,
                            LastName,
                            Alias,
                            DoB
                        FROM Student WHERE StudentID = @StudentID",
                        new { StudentID = studentId });

                if (student == null)
                    return student;

                student.Powers = GetPowersForStudent(connection, studentId);
                student.Weaknesses = GetWeaknessesForStudent(connection, studentId);

                return student;
            }
        }

        public void AddPowerToStudent(int studentId, StudentPower studentPower)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(@"
                    INSERT INTO StudentPower (StudentID, PowerID, Rating)
                    VALUES (@StudentID, @PowerID, @Rating);",
                    new
                    {
                        StudentID = studentId,
                        PowerID = studentPower.PowerID,
                        Rating = studentPower.Rating
                    });
            }
        }

        public void RemovePowerFromStudent(int studentId, int powerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(@"DELETE FROM StudentPower
                    WHERE StudentID = @StudentID AND PowerID = @PowerID",
                    new
                    {
                        StudentID = studentId,
                        PowerID = powerId
                    });
            }
        }

        public void AddWeaknessToStudent(int studentId, StudentWeakness studentWeakness)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(@"
                    INSERT INTO StudentWeakness (StudentID, WeaknessID, RiskLevel)
                    VALUES (@StudentID, @WeaknessID, @RiskLevel);",
                    new
                    {
                        StudentID = studentId,
                        WeaknessID = studentWeakness.WeaknessID,
                        RiskLevel = studentWeakness.RiskLevel
                    });
            }
        }

        public void RemoveWeaknessFromStudent(int studentId, int weaknessId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                connection.Execute(@"DELETE FROM StudentWeakness
                    WHERE StudentID = @StudentID AND WeaknessID = @WeaknessID",
                    new
                    {
                        StudentID = studentId,
                        WeaknessID = weaknessId
                    });
            }
        }

        public List<Student> GetAllStudentsWithPowerType(int powerTypeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<Student>(
                        @"SELECT
                            s.StudentID,
                            s.FirstName,
                            s.LastName,
                            s.Alias,
                            s.DoB
                        FROM StudentPower sp
                        INNER JOIN Student s
                        ON sp.StudentID = s.StudentID
                        INNER JOIN Power p
                        ON sp.PowerID = p.PowerID
                        WHERE PowerTypeID = @PowerTypeID",
                        new { PowerTypeID = powerTypeId })
                    .ToList();
            }
        }

        public List<Student> GetAllStudentsWithWeaknessType(int weaknessTypeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection
                    .Query<Student>(
                        @"SELECT
                            s.StudentID,
                            s.FirstName,
                            s.LastName,
                            s.Alias,
                            s.DoB
                        FROM StudentWeakness sw
                        INNER JOIN Student s
                        ON sw.StudentID = s.StudentID
                        INNER JOIN Weakness w
                        ON sw.WeaknessID = w.WeaknessID
                        WHERE WeaknessTypeID = @WeaknessTypeID",
                        new { WeaknessTypeID = weaknessTypeId })
                    .ToList();
            }
        }

        private List<StudentPower> GetPowersForStudent(
            SqlConnection connection, int studentId)
        {
            return connection.Query<StudentPower>(
                    @"SELECT
                    p.PowerID,
                    p.PowerName,
                    p.PowerDescription,
                    p.PowerTypeID,
                    pt.PowerTypeName,
                    pt.PowerTypeDescription,
                    sp.Rating
                    FROM Power p
                    INNER JOIN StudentPower sp
                    ON p.PowerID = sp.PowerID
                    INNER JOIN PowerType pt
                    ON p.PowerTypeID = pt.PowerTypeID
                    WHERE sp.StudentID = @StudentID",
                    new { StudentID = studentId })
                    .ToList();
        }

        private List<StudentWeakness> GetWeaknessesForStudent(
            SqlConnection connection, int studentId)
        {
            var sql = @"SELECT
                    w.WeaknessID,
                    w.WeaknessName,
                    w.WeaknessDescription,
                    w.WeaknessTypeID,
                    wt.WeaknessTypeName,
                    wt.WeaknessTypeDescription,
                    sw.RiskLevel
                    FROM Weakness w
                    INNER JOIN StudentWeakness sw
                    ON w.WeaknessID = sw.WeaknessID
                    INNER JOIN WeaknessType wt
                    ON w.WeaknessTypeID = wt.WeaknessTypeID
                    WHERE sw.StudentId = @StudentID";

            return connection.Query<StudentWeakness>(sql,
                        new { StudentID = studentId }).ToList();
        }
    }
}