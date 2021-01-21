using System;
using System.Linq;

namespace LinkedInApiClient
{
    public static class LinkedInErrorExtensions
    {
        public static Result<LinkedInError, string> ToStringResult(this LinkedInError error) =>
             Result.Fail(error);

        public static Result<LinkedInError, ILinkedInResponse<T>> ToResult<T>(this LinkedInError error) where T : ILinkedInResponse<T>
            => Result.Fail(error);
    }
}
