using System;
using System.Text.Json.Serialization;

namespace LinkedInApiClient
{
    public class ErrorResponse
    {
        [JsonPropertyName("serviceErrorCode")]
        public int ServiceErrorCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}
