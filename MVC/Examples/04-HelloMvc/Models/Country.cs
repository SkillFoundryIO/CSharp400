namespace HelloMvc.Models
{
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public static class CountryList
    {
        public static List<Country> Countries = new List<Country>
        {
            new Country { Code = "US", Name = "United States" },
            new Country { Code = "UK", Name = "United Kingdom" },
            new Country { Code = "IN", Name = "India" }
        };
    }
}
