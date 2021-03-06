using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkedInApiClient.Types
{
    public class TimeInterval
    {
        public TimeGranularityType TimeGranularityType { get; set; }

        public TimeRange TimeRange { get; set; }
    }

    public static class TimeIntervalExtensions
    {
        /// <summary>
        /// RestLi v1 format
        /// timeIntervals.timeGranularityType=DAY&timeIntervals.timeRange.start=1551398400000&timeIntervals.timeRange.end=1552003200000
        /// </summary>
        /// <param name="timeInterval"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> AsRestLiParametersV1(this TimeInterval timeInterval)
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

        /// <summary>
        /// timeIntervals=(timeRange:(start:1551398400000,end:1552003200000),timeGranularityType:DAY)
        /// </summary>
        /// <param name="timeInterval"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> AsRestLiParametersV2(this TimeInterval timeInterval)
        {
            if (timeInterval == default || !timeInterval.TimeRange.Start.HasValue)
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }
            else
            {
                var builder = new StringBuilder("(timeRange:(start:", 90)
                    .Append(timeInterval.TimeRange.StartToMillisecondString())
                    .Append(",end:")
                    .Append(timeInterval.TimeRange.EndToMillisecondString())
                    .Append("),timeGranularityType:")
                    .Append(Enum.GetName(timeInterval.TimeGranularityType).ToUpper())
                    .Append(")");

                return new Dictionary<string, string>
                {
                    ["timeIntervals"] = builder.ToString(),
                };
            }
        }
    }    
}
