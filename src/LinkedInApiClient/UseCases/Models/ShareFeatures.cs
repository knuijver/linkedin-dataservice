using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Models
{
    public class ShareFeatures
    {
        [JsonPropertyName("hashtags")]
        public LinkedInURN[] Hashtags { get; set; }
    }
}
