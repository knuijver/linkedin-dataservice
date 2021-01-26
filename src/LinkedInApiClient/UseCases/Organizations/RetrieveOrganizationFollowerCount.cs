using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    /// <summary>
    /// The Organization Network Size API provides the ability to retrieve the number of
    /// first-degree connections (followers) for any organization.
    /// </summary>
    public class RetrieveOrganizationFollowerCount : ILinkedInRequest
    {
        public RetrieveOrganizationFollowerCount(LinkedInURN organizationId, string tokenId)
        {
            if (organizationId.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationId)} has an invalid URN Type", nameof(organizationId));

            Url = $"networkSizes/{organizationId.UrlEncode()}";
            QueryParameters = new QueryParameterCollection
            {
                ["edgeType"] = "CompanyFollowedByMember"
            };
            TokenId = tokenId;
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }

        public string TokenId { get; }
    }
}
