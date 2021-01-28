using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace LinkedInApiClient.UseCases
{
    public static class GenericApiQuery
    {
        public static GenericApiQuery<TResponse> Create<TResponse>(string tokenId, string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
            => new GenericApiQuery<TResponse>(tokenId, url, queryParameters);

        public static GenericApiQuery<JsonElement> Create(string tokenId, string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
            => new GenericApiQuery<JsonElement>(tokenId, url, queryParameters);
    }

    public class GenericApiQuery<TResponse> : ILinkedInRequest<TResponse>
    {
        public GenericApiQuery(string tokenId, string url, IEnumerable<KeyValuePair<string, string>> queryParameters)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            QueryParameters = new QueryParameterCollection(queryParameters ?? throw new ArgumentNullException(nameof(queryParameters)));
            TokenId = tokenId;
        }

        public string Url { get; private set; }

        public QueryParameterCollection QueryParameters { get; private set; }

        public string TokenId { get; private set; }
    }
}
