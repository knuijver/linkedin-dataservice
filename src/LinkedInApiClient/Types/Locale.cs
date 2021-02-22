using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.Types
{
    public class Locale
    {
        public static readonly Locale Default = Locale.From(new CultureInfo("en-US"));

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        public override string ToString()
        {
            return
                Language.ToLowerInvariant() +
                '_' +
                Country.ToUpperInvariant();
        }

        public static Locale From(CultureInfo cultureInfo)
        {
            var parts = cultureInfo.TextInfo.CultureName.Split('-');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Culture must contain Language and Country", nameof(cultureInfo));
            }

            return new Locale
            {
                Language = parts[0],
                Country = parts[1]
            };
        }
    }

    public static class LocaleExtensions
    {
        public static IEnumerable<KeyValuePair<string, string>> AsQueryParameters(this Locale locale)
        {
            if (locale == default || string.IsNullOrWhiteSpace(locale.Country) || string.IsNullOrWhiteSpace(locale.Language))
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }
            else
            {
                return new Dictionary<string, string>
                {
                    ["locale.country"] = locale.Country,
                    ["locale.language"] = locale.Language
                };
            }
        }

        public static CultureInfo ToCultureInfo(this Locale @this)
            => new CultureInfo($"{@this.Language}-{@this.Country}");
    }
}
