using LinkedInApiClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IAccessTokenRegistry tokenRegistry;

        public Worker(ILogger<Worker> logger, IAccessTokenRegistry tokenRegistry )
        {
            this.tokenRegistry = tokenRegistry;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var req = LinkedIn.FindAMembersOrganizationAccessControlInformation("urn:fan:token:c6379d94");
            var res = await req.Handle(tokenRegistry, new LinkedInHttpClient(), stoppingToken);

            if (res.IsSuccess)
            {
                foreach (var item in res.Data.Elements)
                {
                    _logger.LogInformation(JsonSerializer.Serialize(item));
                }
            }
            else
            {
                _logger.LogWarning("LINKEDIN FAIL: {Message}", res.Error.Message);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
