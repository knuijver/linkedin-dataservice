using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    /// <summary>
    /// An overlay associated with a media(video, image etc).
    /// </summary>
    public class OverlayMetadata
    {
        [JsonPropertyName("tapTargets")]
        public object[] TapTargets { get; set; }

        [JsonPropertyName("stickers")]
        public ICollection<string> Stickers { get; set; } = new HashSet<string>();

        [JsonPropertyName("overlayTexts")]
        public ICollection<string> OverlayTexts { get; set; } = new HashSet<string>();
    }
}
