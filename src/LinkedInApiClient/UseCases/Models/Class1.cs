using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Models
{
    /// <summary>
    /// TODO: Write a Converter for these types
    /// </summary>
    public class SpecificContent
    {
        [JsonPropertyName("com.linkedin.ugc.ShareContent")]
        public ShareContent UGCShareContent { get; set; }
    }

    public class ShareContent
    {
        [JsonPropertyName("shareCommentary")]
        public ShareCommentary ShareCommentary { get; set; }

        [JsonPropertyName("media")]
        public ICollection<ShareMedia> Media { get; set; } = new HashSet<ShareMedia>();

        [JsonPropertyName("shareFeatures")]
        public ShareFeatures ShareFeatures { get; set; }

        [JsonPropertyName("shareMediaCategory")]
        public string ShareMediaCategory { get; set; }

        [JsonPropertyName("shareCategorization")]
        public Sharecategorization ShareCategorization { get; set; }
    }

    [Obsolete("shareCategorization (deprecated)")]
    public class Sharecategorization
    {
    }
}
