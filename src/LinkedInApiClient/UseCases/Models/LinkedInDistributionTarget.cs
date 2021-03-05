using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class LinkedInDistributionTarget
    {
        [JsonPropertyName("visibleToGuest")]
        public bool VisibleToGuest { get; set; }
    }
}
