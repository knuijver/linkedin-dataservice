using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class RetrieveLifetimeOrganizationPageStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeOrganizationPageStatistics(LinkedInURN organizationId, TimeInterval timeInterval, string tokenId)
        {
            Url = "organizationPageStatistics";
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organization",
                ["organization"] = organizationId.UrlEncode()
            } + timeInterval.AsQueryParameters();
            TokenId = tokenId;
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }
        public string TokenId { get; }
    }
}
