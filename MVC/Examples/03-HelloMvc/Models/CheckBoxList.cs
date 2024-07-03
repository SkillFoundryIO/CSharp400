namespace HelloMvc.Models
{
    public class CheckBoxList
    {
        public List<CheckBoxItem> SelectedCountries { get; set; }
    }

    public class CheckBoxItem
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
