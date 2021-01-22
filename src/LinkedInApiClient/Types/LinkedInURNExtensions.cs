using System;
using System.Linq;

namespace LinkedInApiClient.Types
{
    public static class LinkedInURNExtensions
    {
        public static string UrlEncode(this LinkedInURN urn) => Uri.EscapeDataString(urn.ToString());
    }
}
