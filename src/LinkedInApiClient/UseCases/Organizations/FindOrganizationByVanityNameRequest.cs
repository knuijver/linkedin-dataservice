using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/organization-lookup-api#find-organization-by-vanity-name
    /// </summary>
    public class FindOrganizationByVanityNameRequest : LinkedInRequest
    {
        public FindOrganizationByVanityNameRequest(string vanityName)
        {
            Address = "organizations";
            QueryParameters = new Parameters
            {
                ["q"] = "vanityName",
                ["vanityName"] = vanityName
            };
        }
    }
}
