using System;
using System.Net;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.Types
{
    public class ErrorResponse
    {
        [JsonPropertyName("serviceErrorCode")]
        public int ServiceErrorCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HttpStatusCode Status { get; set; }
    }
}
