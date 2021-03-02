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

    public class UnixTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType == JsonTokenType.Null
                ? default
                : long.TryParse(reader.GetString(), out var value)
                    ? DateTimeOffset.FromUnixTimeMilliseconds(value)
                    : default;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
        }
    }
}
