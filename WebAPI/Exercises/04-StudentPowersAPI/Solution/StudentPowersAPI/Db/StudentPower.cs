namespace StudentPowersAPI.Db
{
    public class StudentPower
    {
        public int PowerID { get; set; }
        public string PowerName { get; set; }
        public string PowerDescription { get; set; }
        public int PowerTypeID { get; set; }
        public string? PowerTypeName { get; set; }
        public string? PowerTypeDescription { get; set; }
        public byte Rating { get; set; }
    }
}
