using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class Visibility
    {
        [JsonPropertyName("com.linkedin.ugc.MemberNetworkVisibility")]
        public string MemberNetworkVisibility { get; set; }
    }
}
