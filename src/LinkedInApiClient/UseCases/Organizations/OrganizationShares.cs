using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class OrganizationShares : ILinkedInRequest
    {
        public OrganizationShares(string tokenId, LinkedInURN organizationUrn)
        {
            if (organizationUrn.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationUrn)} has an invalid URN Type", nameof(organizationUrn));

            TokenId = tokenId;
            Url = "shares";
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "owners",
                ["owners"] = organizationUrn,
                ["sharesPerOwner"] = "1000",
                //["projection"] = "(elements*(id),paging)"
            };
        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }
}
