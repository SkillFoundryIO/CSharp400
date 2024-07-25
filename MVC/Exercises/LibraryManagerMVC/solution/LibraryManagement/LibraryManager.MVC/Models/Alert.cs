namespace LibraryManagement.MVC.Models
{
    public class Alert
    {
        public string Message { get; set; }
        public string Suffix { get; set; }

        public Alert(string message, string suffix)
        {
            Message = message;
            Suffix = suffix;
        }

        public static string CreateSuccess(string message)
        {
            return $"{message}|success";
        }

        public static string CreateError(string message)
        {
            return $"{message}|danger";
        }

        public static string CreateInfo(string message)
        {
            return $"{message}|info";
        }
    }
}
