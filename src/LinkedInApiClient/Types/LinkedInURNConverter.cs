using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.Types
{
    public class LinkedInURNConverter : JsonConverter<LinkedInURN>
    {
        public override LinkedInURN Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType == JsonTokenType.Null
                ? default
                : LinkedInURN.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, LinkedInURN value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
