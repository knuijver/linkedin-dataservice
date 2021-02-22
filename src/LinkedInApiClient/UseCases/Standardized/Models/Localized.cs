using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Standardized
{
    public class Localized
    {
        public static implicit operator string(Localized localized) => localized.ToString();

        [JsonConverter(typeof(LocaleStringConverter))]
        [JsonPropertyName("localized")]
        public LocaleString Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
        /*
        public override string ToString() => ToString(CultureInfo.CurrentUICulture);

        public string ToString(CultureInfo cultureInfo)
        {
            var cultureName = cultureInfo.TextInfo.CultureName.Replace('-', '_');

            return Values.Where(w => string.Equals(w.Key, cultureName, StringComparison.OrdinalIgnoreCase))
                .DefaultIfEmpty(Values.First())
                .Select(s => s.Value)
                .SingleOrDefault();
        }
        */
    }
    /*
    public static class LocalizedExtensions
    {
        public static CultureInfo SelectLocale(this Localized @this, CultureInfo cultureInfo = null)
        {
            var transformed = @this.Values.Select(s => s.Key.Replace('_', '-'));
            var cultureName = (cultureInfo ?? CultureInfo.CurrentUICulture).TextInfo.CultureName;
            return transformed
                .Where(w => string.Equals(w, cultureName, StringComparison.OrdinalIgnoreCase))
                .DefaultIfEmpty(transformed.First())
                .Select(c => new CultureInfo(c))
                .SingleOrDefault();
        }
    }
    */
}
