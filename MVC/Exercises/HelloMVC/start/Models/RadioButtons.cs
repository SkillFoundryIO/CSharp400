namespace HelloMvc.Models
{
    public class RadioButtons
    {
        public List<Country> Countries = CountryList.Countries;
        public string SelectedCountry { get; set; }
    }
}
