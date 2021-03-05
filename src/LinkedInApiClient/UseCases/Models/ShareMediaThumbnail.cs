using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class ShareMediaThumbnail
    {
        /// <summary>
        /// Width of the media in pixels.
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        /// The url of this media.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Height of the media in pixels.
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        /// Not used? Not found in LinkedIn documentation.
        /// </summary>
        [JsonPropertyName("media")]
        public string Media { get; set; }

        /// <summary>
        /// Size of the media in bytes.
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; }

        /// <summary>
        /// The alternate text of this thumbnail. Used for screen reader accessibility.
        /// Is Optional
        /// </summary>
        [JsonPropertyName("altText")]
        public string AltText { get; set; }
    }
}
