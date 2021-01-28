using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class RetrieveLifetimeOrganizationPageStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeOrganizationPageStatistics(string tokenId, LinkedInURN organizationId, TimeInterval timeInterval)
        {
            if (!organizationId.HasValue)
            {
                throw new ArgumentException(nameof(organizationId));
            }

            Url = "organizationPageStatistics";
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organization",
                ["organization"] = organizationId
            } + timeInterval.AsQueryParameters();
            TokenId = tokenId;
        }

        public string TokenId { get; }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }
    }
}
