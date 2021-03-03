using System;
using System.Linq;
using LinkedInApiClient.Types;

#nullable enable

namespace LinkedInApiClient.UseCases.Standardized
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/shared/references/v2/standardized-data/seniorities#get_all
    /// </summary>
    public class GetAllSenioritiesRequest : LinkedInRequest
    {
        public GetAllSenioritiesRequest(Locale locale)
        {
            Address = "seniorities";
            QueryParameters = Parameters.EmptyParameters +
                locale.AsQueryParameters();
        }
    }
}
