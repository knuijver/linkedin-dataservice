using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Organizations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class OrganizationSharesRequest : LinkedInRequest
    {
        public OrganizationSharesRequest(LinkedInURN organizationUrn)
        {
            if (organizationUrn.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationUrn)} has an invalid URN Type", nameof(organizationUrn));

            Address = "shares";
            QueryParameters = new Parameters
            {
                ["q"] = "owners",
                ["owners"] = organizationUrn,
                ["sharesPerOwner"] = "1000",
                ["projection"] = "(*,elements*(*,created(*,actor~(localizedLastName,localizedFirstName,vanityName,localizedHeadline))))"
            };
        }
    }
}
