using System;
using System.Linq;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.AccessControl.Models;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.AccessControl
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/organization-access-control#find-organization-administrators
    /// Projection: https://docs.microsoft.com/en-us/linkedin/shared/api-guide/concepts/projections?context=linkedin/marketing/context
    /// </summary>
    public class FindOrganizationAdministrators : ILinkedInRequest<Paged<OrganizationRoleEntry>>
    {
        public FindOrganizationAdministrators(string tokenId, LinkedInURN organizationUrn)
        {
            if (organizationUrn.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationUrn)} has an invalid URN Type", nameof(organizationUrn));

            TokenId = tokenId;
            Url = "organizationAcls";
            QueryParameters = new Parameters
            {
                ["q"] = "organization",
                ["organization"] = organizationUrn,
                ["role"] = "ADMINISTRATOR", // Limit results to specific roles, such as ADMINISTRATOR or DIRECT_SPONSORED_CONTENT_POSTER
                ["state"] = "APPROVED", // Limit results to specific role states, such as APPROVED or REQUESTED.
                ["projection"] = "(*,elements*(*,roleAssignee~(localizedFirstName, localizedLastName), organization~(localizedName)))"
            };
        }

        public string TokenId { get; }
        public string Url { get; }
        public Parameters QueryParameters { get; }
    }
}
