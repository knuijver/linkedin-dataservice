using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class RetrieveAnAdministeredOrganization : ILinkedInRequest
    {
        public RetrieveAnAdministeredOrganization(string tokenId, LinkedInURN organizationId)
        {
            if (organizationId.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationId)} has an invalid URN Type", nameof(organizationId)); 

            TokenId = tokenId;
            Url = $"organizations/{organizationId.Id}";
        }

        public string Url { get; }

        public Parameters QueryParameters { get; } = Parameters.EmptyParameters;

        public string TokenId { get; }
    }
}
