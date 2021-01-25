using System.Net;

namespace LinkedInApiClient
{
    public class ErrorResponse
    {
        public int ServiceErrorCode { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class LinkedInError
    {
        public LinkedInError(HttpStatusCode statusCode, string message, int serviceErrorCode)
        {
            this.Message = message;
            this.StatusCode = statusCode;
            ServiceErrorCode = serviceErrorCode;
        }

        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public int ServiceErrorCode { get; private set; }

        public static LinkedInError With(HttpStatusCode statusCode, string message)
        {
            return new LinkedInError(statusCode, message, -1);
        }
        public static LinkedInError From(ErrorResponse error)
        {
            return new LinkedInError((HttpStatusCode)error.Status, error.Message, error.ServiceErrorCode);
        }
    }
}
