using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class RetrieveAnAdministeredOrganizationRequest : LinkedInRequest
    {
        public RetrieveAnAdministeredOrganizationRequest(LinkedInURN organizationId)
        {
            if (organizationId.EntityType != "organization")
                throw new ArgumentException($"{nameof(organizationId)} has an invalid URN Type", nameof(organizationId)); 

            Address = $"organizations/{organizationId.Id}";
        }
    }
}
