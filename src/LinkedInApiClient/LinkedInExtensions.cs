using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient
{
    public static class LinkedInExtensions
    {
        public static DelayedError<LinkedInError> ToResult(this LinkedInError error) => Result.Fail(error);

        public static Uri HttpRequestUrl(this ILinkedInRequest request)
        {
            return new Uri(request.QueryParameters.ToUrlQueryString(request.Url));
        }
    }
}
