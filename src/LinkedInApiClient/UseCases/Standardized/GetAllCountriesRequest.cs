using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Standardized.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Standardized
{
    public class GetAllCountriesRequest : LinkedInRequest //Paged<Country>
    {
        public GetAllCountriesRequest(Locale locale)
        {
            Address = "countries";
            QueryParameters = Parameters.EmptyParameters
                + locale.AsQueryParameters();
            QueryParameters.Add("projection", "(*,elements*(*,countryGroup~(*)))");
        }
    }
}
