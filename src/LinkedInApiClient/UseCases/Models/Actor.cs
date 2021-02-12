using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class Actor
    {
        [JsonPropertyName("localizedLastName")]
        public string LastName { get; set; }

        [JsonPropertyName("vanityName")]
        public string VanityName { get; set; }

        [JsonPropertyName("localizedHeadline")]
        public string Headline { get; set; }

        [JsonPropertyName("localizedFirstName")]
        public string FirstName { get; set; }
    }
}
