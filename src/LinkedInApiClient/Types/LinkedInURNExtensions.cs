using System;

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
    }
}
