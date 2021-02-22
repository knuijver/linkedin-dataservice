using System;
using System.Linq;
using LinkedInApiClient.Types;

#nullable enable

namespace LinkedInApiClient.UseCases.Standardized
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/shared/references/v2/standardized-data/seniorities#get_all
    /// </summary>
    public class GetAllSeniorities : ILinkedInRequest
    {
        public GetAllSeniorities(string tokenId, Locale locale)
        {
            TokenId = tokenId;
            Url = "seniorities";
            QueryParameters = QueryParameterCollection.EmptyParameters +
                locale.AsQueryParameters();
        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }
}
