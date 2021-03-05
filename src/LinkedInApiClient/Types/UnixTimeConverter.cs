using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.Types
{
    public class UnixTimeConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType == JsonTokenType.Null
                ? default
                : reader.TokenType == JsonTokenType.Number
                    ? DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64())
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
