using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    public class LinkedInRequest
    {
        public LinkedInRequest(string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            QueryParameters = queryParameters ?? throw new ArgumentNullException(nameof(queryParameters));
        }

        public string Url { get; private set; }
        public IEnumerable<KeyValuePair<string, string>> QueryParameters { get; private set; }
    }
}
