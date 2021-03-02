using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Standardized.Models
{
    public class CountryGroup
    {
        [JsonPropertyName("name")]
        public Localized name { get; set; }
        [JsonPropertyName("$URN")]
        public LinkedInURN URN { get; set; }
        [JsonPropertyName("countryGroupCode")]
        public LinkedInURN countryGroupCode { get; set; }
    }
}
