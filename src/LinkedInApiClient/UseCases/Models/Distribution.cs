﻿using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.Organizations.Models
{
    public class Distribution
    {
        [JsonPropertyName("linkedInDistributionTarget")]
        public LinkedInDistributionTarget LinkedInDistributionTarget { get; set; }
    }
}
