using System;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient
{
    public static class LinkedInErrorExtensions
    {
        public static DelayedError<LinkedInError> ToResult(this LinkedInError error) => Result.Fail(error);
    }
}
