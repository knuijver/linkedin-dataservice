using System;

namespace LinkedInApiClient
{
    public class LinkedInCaughtException : LinkedInError
    {
        public LinkedInCaughtException(string message, Exception exception)
            : base(message)
        {
            Exception = exception;
        }

        public Exception Exception { get; set; }

    }
}
