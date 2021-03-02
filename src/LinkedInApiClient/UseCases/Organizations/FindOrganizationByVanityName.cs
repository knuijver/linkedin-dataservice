using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/organization-lookup-api#find-organization-by-vanity-name
    /// </summary>
    public class FindOrganizationByVanityName : ILinkedInRequest
    {
        public FindOrganizationByVanityName(string tokenId, string vanityName)
        {
            TokenId = tokenId;
            Url = "organizations";
            QueryParameters = new Parameters
            {
                ["q"] = "vanityName",
                ["vanityName"] = vanityName
            };
        }

        public string TokenId { get; }

        public string Url { get; }

        public Parameters QueryParameters { get; }
    }
}
