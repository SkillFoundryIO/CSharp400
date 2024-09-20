namespace StudentPowersAPI.Db
{
    public class StudentDetail
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public DateTime DoB { get; set; }
        public List<StudentPower> Powers { get; set; } // List of powers
        public List<StudentWeakness> Weaknesses { get; set; }
    }
}
