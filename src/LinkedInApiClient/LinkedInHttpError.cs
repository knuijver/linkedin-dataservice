using System;
using System.Net;

namespace LinkedInApiClient
{
    public class LinkedInHttpError : LinkedInError
    {
        public LinkedInHttpError(HttpStatusCode statusCode, string message, int serviceErrorCode)
            : base(message)
        {
            this.StatusCode = statusCode;
            this.ServiceErrorCode = serviceErrorCode;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public int ServiceErrorCode { get; private set; }

        public static LinkedInError With(HttpStatusCode statusCode, string message)
        {
            return new LinkedInHttpError(statusCode, message, -1);
        }
        public static LinkedInError From(ErrorResponse error)
        {
            return new LinkedInHttpError((HttpStatusCode)error.Status, error.Message, error.ServiceErrorCode);
        }
    }
}
