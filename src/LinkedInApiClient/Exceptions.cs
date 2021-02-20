using System;
using System.Net;
using System.Text.Json.Serialization;

namespace LinkedInApiClient
{
    public abstract class LinkedInError
    {
        public LinkedInError(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }

    public class LinkedInAccessTokenError : LinkedInError
    {
        public LinkedInAccessTokenError(TokenFailure failure)
            : base(failure.Message)
        {
            this.FailureType = failure.FailureType;
        }

        public string FailureType { get; }

        public static LinkedInError Create(TokenFailure error)
        {
            return new LinkedInAccessTokenError(error);
        }
    }

    public static class LinkedInErrorExtensions
    {
        public static bool TryErrorType<T>(this LinkedInError error, out T actualError)
            where T : LinkedInError
        {
            if (error is T tmp)
            {
                actualError = tmp;
                return true;
            }
            else
            {
                actualError = default;
                return false;
            }
        }

        public static Result<LinkedInError, T> IfHttpError<T>(this Result<LinkedInError, T> error, Action<LinkedInHttpError> handle)
        {
            if (!error.IsSuccess && TryErrorType<LinkedInHttpError>(error.Error, out var httpError))
            {
                handle(httpError);
                return default;
            }

            return error;
        }
        public static Result<LinkedInError, T> IfException<T>(this Result<LinkedInError, T> error, Action<LinkedInCaughtException> handle)
        {
            if (!error.IsSuccess && TryErrorType<LinkedInCaughtException>(error.Error, out var httpError))
            {
                handle(httpError);
                return default;
            }

            return error;
        }
        public static Result<LinkedInError, T> IfTokenFailure<T>(this Result<LinkedInError, T> error, Action<LinkedInAccessTokenError> handle)
        {
            if (!error.IsSuccess && TryErrorType<LinkedInAccessTokenError>(error.Error, out var httpError))
            {
                handle(httpError);
                return default;
            }

            return error;
        }
    }
}
