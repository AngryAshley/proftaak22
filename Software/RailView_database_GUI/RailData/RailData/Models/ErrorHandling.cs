using System.Collections.Generic;

namespace RailData.Models
{
    public class ErrorHandling
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public string Error()
        {
            string completeError = "<p class='mt-3 alert alert-" + ErrorType + "' role='alert'>" + ErrorMessage + "</p>";

            return completeError;
        }
    }
}
