namespace StudentPowersAPI.Db
{
    public class StudentWeakness
    {
        public int WeaknessID { get; set; }
        public string WeaknessName { get; set; }
        public string WeaknessDescription { get; set; }
        public int WeaknessTypeID { get; set; }
        public string? WeaknessTypeName { get; set; }
        public string? WeaknessTypeDescription { get; set; }
        public byte RiskLevel { get; set; }
    }
}
