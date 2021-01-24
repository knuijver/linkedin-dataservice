using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public static class GenericApiQuery
    {
        public static GenericApiQuery<TResponse> Create<TResponse>(string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
            => new GenericApiQuery<TResponse>(url, queryParameters);
    }

    public class GenericApiQuery<TResponse> : ILinkedInRequest, ILinkedInResponse<TResponse>
    {
        public GenericApiQuery(string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            QueryParameters = new QueryParameterCollection(queryParameters ?? throw new ArgumentNullException(nameof(queryParameters)));
        }

        public string Url { get; private set; }
        public QueryParameterCollection QueryParameters { get; private set; }
    }
}
