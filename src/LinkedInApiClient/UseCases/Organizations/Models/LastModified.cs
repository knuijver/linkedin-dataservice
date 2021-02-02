using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Organizations.Models
{
    public class Created
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("actor~")]
        public Actor Actor { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }
    public class LastModified
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }

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
