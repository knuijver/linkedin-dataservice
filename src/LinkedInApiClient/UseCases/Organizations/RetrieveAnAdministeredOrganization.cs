using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class RetrieveAnAdministeredOrganization : ILinkedInRequest
    {
        public RetrieveAnAdministeredOrganization(LinkedInURN organizationId, string tokenId)
        {
            if (organizationId.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationId)} has an invalid URN Type", nameof(organizationId)); 

            TokenId = tokenId;
            Url = $"organizations/{organizationId.UrlEncode()}";
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; } = QueryParameterCollection.EmptyParameters;

        public string TokenId { get; }
    }
}
