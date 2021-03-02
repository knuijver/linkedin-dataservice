using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable

namespace LinkedInApiClient.UseCases.Standardized.Models
{
    public class LocaleStringConverter : JsonConverter<LocaleString>
    {
        public override LocaleString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            LocaleString localeString = new LocaleString();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return localeString;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException("field names can't be empty");
                    var parts = propertyName.Split('_');
                    localeString.Locale = new Locale()
                    {
                        Language = parts[0],
                        Country = parts[1]
                    };

                    reader.Read();
                    localeString.Value = reader.GetString();
                }
                else
                {
                    throw new JsonException();
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, LocaleString value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(value.Locale.ToString(), value.Value);

            writer.WriteEndObject();
        }
    }
}
