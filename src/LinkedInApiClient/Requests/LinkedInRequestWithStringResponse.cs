using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient
{
    public interface ILinkedInRequest
    {
        string Url { get; }
        QueryParameterCollection QueryParameters { get; }
    }
    public interface ILinkedInResponse<T>
    {
    }

    public class LinkedInRequestWithStringResponse : ILinkedInRequest, ILinkedInResponse<string>
    {
        public LinkedInRequestWithStringResponse(string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            QueryParameters = new QueryParameterCollection( queryParameters ?? throw new ArgumentNullException(nameof(queryParameters)));
        }

        public string Url { get; private set; }
        public QueryParameterCollection QueryParameters { get; private set; }
    }
}
