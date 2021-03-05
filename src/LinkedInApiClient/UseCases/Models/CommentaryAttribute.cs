using LinkedInApiClient.UseCases.Standardized.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class CommentaryAttribute
    {
        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("value")]
        //[JsonConverter(typeof(NameValuePairConverter))]
        public AttributeValue Value { get; set; }
    }
    public class AttributeValue
    {
        [JsonPropertyName("com.linkedin.common.HashtagAttributedEntity")]
        [JsonConverter(typeof(NameValuePairConverter))]
        public IDictionary<string, string> HashtagAttributedEntity { get; set; }

        [JsonPropertyName("com.linkedin.common.MemberAttributedEntity")]
        [JsonConverter(typeof(NameValuePairConverter))]
        public IDictionary<string, string> MemberAttributedEntity { get; set; }

        [JsonPropertyName("com.linkedin.common.CompanyAttributedEntity")]
        [JsonConverter(typeof(NameValuePairConverter))]
        public IDictionary<string, string> CompanyAttributedEntity { get; set; }
    }
}
