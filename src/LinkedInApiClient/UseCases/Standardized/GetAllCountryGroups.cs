using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.Standardized
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/shared/references/v2/standardized-data/locations/country-groups#get-all
    /// </summary>
    public class GetAllCountryGroups : ILinkedInRequest<Paged<CountryGroup>>
    {
        public GetAllCountryGroups(string tokenId, Locale locale)
        {
            TokenId = tokenId;
            Url = "countries";
            QueryParameters = QueryParameterCollection.EmptyParameters
                + locale.AsQueryParameters();
        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }
}
