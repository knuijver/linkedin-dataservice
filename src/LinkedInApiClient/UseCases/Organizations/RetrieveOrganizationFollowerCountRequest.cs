using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// The Organization Network Size API provides the ability to retrieve the number of
    /// first-degree connections (followers) for any organization.
    /// </summary>
    public class RetrieveOrganizationFollowerCountRequest : LinkedInRequest
    {
        public RetrieveOrganizationFollowerCountRequest(LinkedInURN organizationId)
        {
            if (organizationId.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationId)} has an invalid URN Type", nameof(organizationId));

            Address = $"networkSizes/{organizationId}";
            QueryParameters = new Parameters
            {
                ["edgeType"] = "CompanyFollowedByMember"
            };
        }
    }
}
