using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/organization-lookup-api#find-organization-by-email-domain
    /// </summary>
    public class FindOrganizationByEmailDomainRequest : LinkedInRequest
    {
        public FindOrganizationByEmailDomainRequest(string emailDomain)
        {
            Address = "organizations";
            QueryParameters = new Parameters
            {
                ["q"] = "emailDomain",
                ["emailDomain"] = emailDomain
            };
        }
    }
}
