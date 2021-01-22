using System;
using System.Linq;

namespace LinkedInApiClient.Types
{
    public struct TimeRange
    {
        public DateTimeOffset? Start { get; set; }
        public DateTimeOffset? End { get; set; }

        /// <summary>
        /// milliseconds since epoch
        /// </summary>
        /// <returns></returns>
        public string StartToMillisecondString() => Start?.ToUnixTimeMilliseconds().ToString();

        /// <summary>
        /// milliseconds since epoch
        /// </summary>
        /// <returns></returns>
        public string EndToMillisecondString() => End?.ToUnixTimeMilliseconds().ToString();
    }
}
