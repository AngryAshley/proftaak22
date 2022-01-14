namespace RailData.Models
{
    public class ErrorHandling
    {
        // Public error message.
        public string ErrorMessage { get; set; }

        public string Error()
        {
            // Return the error message where the function is called.
            return ErrorMessage;
        }
    }
}
