using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class LandingPage
    {
        [JsonPropertyName("landingPageTitle")]
        public string LandingPageTitle { get; set; }

        [JsonPropertyName("landingPageUrl")]
        public string LandingPageUrl { get; set; }
    }
}
