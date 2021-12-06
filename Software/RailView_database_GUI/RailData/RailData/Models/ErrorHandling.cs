namespace RailData.Models
{
    public class ErrorHandling
    {
        public string ErrorMessage { get; set; }

        public string Error()
        {
            return ErrorMessage;
        }
    }
}
