using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;

namespace LinkedInWorkerService
{
    public class SchedulingWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CronExpression expression = CronExpression.Parse("* * * * *", CronFormat.Standard);

            DateTimeOffset? next = expression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Utc);

            return Task.CompletedTask;
        }
    }
}
