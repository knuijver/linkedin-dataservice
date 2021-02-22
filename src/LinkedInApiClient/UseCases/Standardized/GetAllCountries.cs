using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Standardized
{
    public class GetAllCountries : ILinkedInRequest<Paged<Country>>
    {
        public GetAllCountries(string tokenId, Locale locale)
        {
            TokenId = tokenId;
            Url = "countries";
            QueryParameters = QueryParameterCollection.EmptyParameters
                + locale.AsQueryParameters();
            QueryParameters.Add("projection", "(*,elements*(*,countryGroup~(*)))");
        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }
}
