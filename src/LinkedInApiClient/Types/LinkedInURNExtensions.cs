using System;
using System.Linq;
using System.Collections.Generic;

namespace LinkedInApiClient.Types
{
    public static class LinkedInURNExtensions
    {
        /// <summary>
        /// When using RestLi protocol version 2.0.0, encode the URN for use in an URL path
        /// </summary>
        /// <param name="urn"></param>
        /// <returns></returns>
        public static string UrlEncode(this LinkedInURN urn) => Uri.EscapeDataString(urn.ToString());

        public static bool HasReferences(this LinkedInURN urn)
        {
            return (urn.Id.StartsWith('(') && new[] { "urn", ":", "," }.Count(c => urn.Id.Contains(c) == true) >= 2);
        }

        public static IEnumerable<LinkedInURN> IdReferences(this LinkedInURN urn)
        {
            var str = urn.Id.Substring(1, urn.Id.Length - 1);
            return str.Split(",")
                    .Select(LinkedInURN.Parse);
        }
    }
}
