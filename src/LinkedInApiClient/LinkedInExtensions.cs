using LinkedInApiClient.Types;
using System;

namespace LinkedInApiClient
{
    public static class LinkedInExtensions
    {
        public static DelayedError<LinkedInError> ToResult(this LinkedInError error) => Result.Fail(error);

        public static Uri HttpRequestUrl(this ILinkedInRequest request)
        {
            return new Uri(request.QueryParameters.ToUrlQueryString(request.Url));
        }

        public static Uri HttpRequestUrl(this IBaseApiRequest request)
        {
            return new Uri(request.QueryParameters.ToUrlQueryString(request.Url));
        }

        public static void Validate(this IBaseApiRequest request)
        {
            if (request.QueryParameters == null)
            {
                throw new ArgumentNullException(nameof(request.QueryParameters));
            }

            if (request.Url == null || request.Url.Length == 0)
            {
                throw new ArgumentException(nameof(request.Url));
            }

            //if (request.TokenId != null && request.TokenId.Length == 0)
            //{
            //    throw new ArgumentException($"{nameof(request.TokenId)} must be Null or No Whitespace");
            //}
        }
    }
}
