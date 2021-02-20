using System;

namespace LinkedInApiClient
{
    public class TokenFailure
    {
        public TokenFailure()
        {
        }

        public TokenFailure(string failureType, string message)
        {
            FailureType = failureType;
            Message = message;
        }

        public string FailureType { get; }
        public string Message { get; }

        public static TokenFailure Why(string failureType, string message)
        {
            return new TokenFailure(failureType, message);
        }
    }
}
