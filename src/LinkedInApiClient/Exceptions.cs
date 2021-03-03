using System;
using System.Net;
using System.Text.Json.Serialization;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient
{
    public enum LinkedInErrorType
    {
        None,
        Protocol,
        Http,
        Exception,
        AccessToken
    }

    public class LinkedInError
    {
        public LinkedInError(string message)
        {
            this.ReasonText = message;
        }

        public string ReasonText { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        public int ServiceErrorCode { get; private set; }

        public Exception Exception { get; private set; }

        public LinkedInErrorType ErrorType { get; private set; } = LinkedInErrorType.None;

        public static LinkedInError FromException(Exception ex, string errorMessage = default)
        {
            return new LinkedInError(errorMessage ?? ex.Message)
            {
                Exception = ex,
                ErrorType = LinkedInErrorType.Exception
            };
        }
        public static LinkedInError FromResponseError(LinkedInResponse response)
        {
            return new LinkedInError(response.Error);
        }

        public static LinkedInError FromTokenResponse(string message)
        {
            return new LinkedInError(message)
            {
                ErrorType = LinkedInErrorType.AccessToken
            };
        }
    }

    public static class LinkedInErrorExtensions
    {
        public static Result<LinkedInError, T> IfError<T>(this Result<LinkedInError, T> error, Action<LinkedInError> handle)
        {
            if (!error.IsSuccess)
            {
                handle(error.Error);
                return default;
            }

            return error;
        }
    }
}
