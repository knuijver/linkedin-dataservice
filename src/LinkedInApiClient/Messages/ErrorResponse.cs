using System;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.Messages
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
