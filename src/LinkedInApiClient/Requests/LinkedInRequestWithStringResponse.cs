using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    public interface ILinkedInRequest
    {
        string Url { get; }
        IEnumerable<KeyValuePair<string, string>> QueryParameters { get; }
    }
    public interface ILinkedInResponse<T>
    {
    }

    public class LinkedInRequestWithStringResponse : ILinkedInRequest, ILinkedInResponse<string>
    {
        public LinkedInRequestWithStringResponse(string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            QueryParameters = queryParameters ?? throw new ArgumentNullException(nameof(queryParameters));
        }

        public string Url { get; private set; }
        public IEnumerable<KeyValuePair<string, string>> QueryParameters { get; private set; }
    }
}
