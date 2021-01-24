using System;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient
{
    public interface ILinkedInRequest
    {
        string Url { get; }
        QueryParameterCollection QueryParameters { get; }
    }
}
