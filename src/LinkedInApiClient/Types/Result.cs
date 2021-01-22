using System;
using System.Linq;

namespace LinkedInApiClient.Types
{
    public static class Result
    {
        public static DelayedData<TResult> Success<TResult>(TResult result)
        {
            return new DelayedData<TResult>(result);
        }
        public static DelayedError<TError> Fail<TError>(TError error)
        {
            return new DelayedError<TError>(error);
        }
    }
}
