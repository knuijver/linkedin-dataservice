using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient.Types
{
    public class TimeInterval
    {
        public TimeGranularityType TimeGranularityType { get; set; }

        public TimeRange TimeRange { get; set; }
    }

    public static class TimeIntervalExtensions
    {
        public static IEnumerable<KeyValuePair<string, string>> AsQueryParameters(this TimeInterval timeInterval)
        {
            if (timeInterval == default || !timeInterval.TimeRange.Start.HasValue)
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }
            else
            {
                return new Dictionary<string, string>
                {
                    ["timeIntervals.timeGranularityType"] = Enum.GetName(timeInterval.TimeGranularityType).ToUpper(),
                    ["timeIntervals.timeRange.start"] = timeInterval.TimeRange.StartToMillisecondString(),
                    ["timeIntervals.timeRange.end"] = timeInterval.TimeRange.EndToMillisecondString()
                };
            }
        }
    }

}
