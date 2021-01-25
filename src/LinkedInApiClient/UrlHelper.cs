using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    static class UrlHelper
    {
        public static string AppendQueryToUrl(string url, IEnumerable<KeyValuePair<string, string>> query)
        {
            if (!query.Any())
            {
                return url;
            }
            else
            {
                return url
                    + (url.Contains("?") ? "&" : "?")
                    + string.Join("&", query.Select(x => Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value)));
            }
        }

        public static string Combine(string baseUri, string path)
            => Combine(new Uri(baseUri), path).ToString();

        public static Uri Combine(Uri baseUri, string path)
        {
            var builder = new UriBuilder(baseUri);

            builder.Path = (builder.Path.EndsWith("/"))
                ? string.Concat(builder.Path, path)
                : string.Concat(builder.Path, "/", path);

            return builder.Uri;
        }

        public static Uri CreateUri(string url, IEnumerable<KeyValuePair<string, string>> query) =>
             new Uri(AppendQueryToUrl(url, query));

    }
}
