using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.AccessControl.Models
{
    public class OrganizationInfo
    {
        [JsonPropertyName("localizedName")]
        public string Name { get; set; }
    }
}
