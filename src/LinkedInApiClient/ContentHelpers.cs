using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace LinkedInApiClient
{
    public static class ContentHelpers
    {
        /// <summary>
        /// Create an FormUrlEncodedContent from a KeyValuePair collection with a non-nullable Key.
        /// example Dictionary&lt;string,string?&gt;
        /// <see cref="https://github.com/dotnet/runtime/issues/38494"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HttpContent FormData(IEnumerable<KeyValuePair<string, string?>> data)
            => new FormUrlEncodedContent((IEnumerable<KeyValuePair<string?, string?>>)data);


        internal static HttpContent JsonContent(object content)
        {
            var json = content is string str ? str : JsonSerializer.Serialize(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
