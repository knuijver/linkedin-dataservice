using LinkedInApiClient.Messages;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.AccessControl.Models;
using LinkedInApiClient.UseCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.AccessControl
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/organization-access-control#find-a-members-organization-access-control-information
    /// Projection: https://docs.microsoft.com/en-us/linkedin/shared/api-guide/concepts/projections?context=linkedin/marketing/context
    /// </summary>
    public class FindAMembersOrganizationAccessControlInformationRequest : LinkedInRequest
    {
        public FindAMembersOrganizationAccessControlInformationRequest(string tokenId)
        {
            TokenId = tokenId;
            Url = "organizationAcls";
            QueryParameters = new Parameters
            {
                ["q"] = "roleAssignee",
                ["projection"] = "(*,elements*(*,roleAssignee~(localizedFirstName, localizedLastName), organization~(localizedName)))"
            };
        }

        public string TokenId { get; }
        public string Url { get; }
        public Parameters QueryParameters { get; }
    }

    public class FindAMembersOrganizationAccessControlInformationResponse : LinkedInResponse //Paged<OrganizationRoleEntry>
    {
    }
}
