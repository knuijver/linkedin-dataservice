using System;
using System.Linq;

namespace LinkedInApiClient
{
    public static class LinkedInErrorExtensions
    {
        public static IResult<LinkedInError, string> ToStringResult(this LinkedInError error) =>
             Result.Error<LinkedInError, string>(error);
    }
}
