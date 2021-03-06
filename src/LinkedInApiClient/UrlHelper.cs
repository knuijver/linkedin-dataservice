using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinkedInApiClient
{
    static class UrlHelper
    {
        /// <summary>
        /// Append all key, value pairs as querystring to the given URL.
        /// We don't check for any duplicate keys on the given URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string AppendQueryToUrl(string url, IEnumerable<KeyValuePair<string, string>> query, bool encodeValues = true)
        {
            //var f = query.Where(w => !string.IsNullOrWhiteSpace(w.Value));
            if (!query.Any())
            {
                return url;
            }
            else
            {
                return url
                    + (url.Contains("?") ? "&" : "?")
                    + string.Join("&", query.Select(x => Uri.EscapeDataString(x.Key) + "=" + (x.Value == null ? "" : encodeValues ? Uri.EscapeDataString(x.Value) : x.Value)));
            }
        }

        [DebuggerStepThrough]
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

        [DebuggerStepThrough]
        public static Uri CreateUri(string url, IEnumerable<KeyValuePair<string, string>> query) =>
             new Uri(AppendQueryToUrl(url, query));

    }
}
