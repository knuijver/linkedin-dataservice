using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.Types
{
    public struct TimeRange
    {
        [JsonPropertyName("start")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTimeOffset? Start { get; set; }

        [JsonPropertyName("end")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTimeOffset? End { get; set; }

        /// <summary>
        /// milliseconds since epoch
        /// </summary>
        /// <returns></returns>
        public string StartToMillisecondString() => Start?.ToUnixTimeMilliseconds().ToString();

        /// <summary>
        /// milliseconds since epoch
        /// </summary>
        /// <returns></returns>
        public string EndToMillisecondString() => End?.ToUnixTimeMilliseconds().ToString();
    }
}
