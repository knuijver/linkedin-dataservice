using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable

namespace LinkedInApiClient.UseCases.Standardized.Models
{
    public class NameValuePairConverter : JsonConverter<IDictionary<string, string?>>
    {
        public override IDictionary<string, string?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var result = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return result;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException("field names can't be empty");

                    reader.Read();
                    result[propertyName] = reader.GetString();
                }
                else
                {
                    throw new JsonException();
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<string, string?> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var item in value)
            {
                writer.WriteString(item.Key, item.Value);
            }

            writer.WriteEndObject();
        }
    }
    /*
    public class NameValuePairConverter<T> : JsonConverter<IDictionary<string, T?>>
    {
        public override IDictionary<string, T?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var result = new Dictionary<string, T?>(StringComparer.OrdinalIgnoreCase);
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return result;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException("field names can't be empty");

                    
                    reader.Read();
                    result[propertyName] = reader.GetString();
                }
                else
                {
                    throw new JsonException();
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<string, T?> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var item in value)
            {
                writer.WriteString(item.Key, item.Value);
            }

            writer.WriteEndObject();
        }
    }
    */
}
