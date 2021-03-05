using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Standardized.Models;

namespace LinkedInApiClient.UseCases.Standardized
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/shared/references/v2/standardized-data/locations/country-groups#get-all
    /// </summary>
    public class GetAllCountryGroupsRequest : LinkedInRequest //Paged<CountryGroup>
    {
        public GetAllCountryGroupsRequest(Locale locale)
        {
            Address = "countries";
            QueryParameters = Parameters.EmptyParameters
                + locale.AsQueryParameters();
        }
    }
}
