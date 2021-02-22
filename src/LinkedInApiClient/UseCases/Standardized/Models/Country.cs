using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Standardized
{
    public class Country
    {
        [JsonPropertyName("countryGroup")]
        public LinkedInURN CountryGroupUrn { get; set; }

        [JsonPropertyName("countryGroup~")]
        public CountryGroup CountryGroup { get; set; }

        [JsonPropertyName("countryCode")]
        public string countryCode { get; set; }

        [JsonPropertyName("name")]
        public Localized name { get; set; }

        [JsonPropertyName("$URN")]
        public LinkedInURN URN { get; set; }
    }
}
