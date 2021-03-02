using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/organization-lookup-api#find-organization-by-email-domain
    /// </summary>
    public class FindOrganizationByEmailDomain : ILinkedInRequest
    {
        public FindOrganizationByEmailDomain(string tokenId, string emailDomain)
        {
            QueryParameters = new Parameters
            {
                ["q"] = "emailDomain",
                ["emailDomain"] = emailDomain
            };
            TokenId = tokenId;
        }
        public string TokenId { get; }

        public string Url { get; } = "organizations";

        public Parameters QueryParameters { get; }
    }
}
