using LinkedInApiClient;
using LinkedInApiClient.Store;
using LinkedInApiClient.Types;
using LinkedInApiClient.Extensions;
using LinkedInWorkerService.Common.Types;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.AccessControl;

namespace LinkedInWorkerService
{
    public class InboundMessageWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
    public class LinkedInDataCollectionWorker : BackgroundService
    {
        private readonly ILogger<LinkedInDataCollectionWorker> _logger;
        private readonly IAccessTokenRegistry tokenRegistry;

        public LinkedInDataCollectionWorker(ILogger<LinkedInDataCollectionWorker> logger, IAccessTokenRegistry tokenRegistry)
        {
            this.tokenRegistry = tokenRegistry;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new HttpClient()
                .UseDefaultLinkedInBaseUrl();

            var accessTokens = await tokenRegistry.ListAsync(CancellationToken.None);
            // all Me
            // or find organizations
            //LinkedIn.
            // administrated organizations

            //var (error, element) = (await GenericApiQuery.Create(URN.TokenStoreUrn("token", "c6379d94"), "", QueryParameterCollection.EmptyParameters)
            //    .Handle(tokenRegistry, client, stoppingToken));

            if (accessTokens.Try(out var tokens))
            {
                foreach (var token in tokens)
                {
                    var me = await client.GetMyProfileAsync(new LinkedInApiClient.UseCases.People.GetMyProfileRequest(), stoppingToken);
                    if (!me.IsError)
                    {

                    }
                }
            }

            var accessTokenResult = await tokenRegistry.AccessTokenAsync(URN.TokenStoreUrn("token", "c6379d94"), stoppingToken);
            if (accessTokenResult.Try(out string accesToken))
            {
                await client.FindOrganizationAdministratorsAsync(
                    new FindOrganizationAdministratorsRequest(CommonURN.OrganizationId(""))
                        .WithAccessToken(accesToken));

                var res = await client.FindAMembersOrganizationAccessControlInformationAsync(
                    new FindAMembersOrganizationAccessControlInformationRequest()
                        .WithAccessToken(accesToken));

                if (res.IsSuccess)
                {
                    foreach (var item in res.Data.Elements)
                    {
                        _logger.LogInformation(JsonSerializer.Serialize(item));
                    }
                }
                else
                {
                    _logger.LogWarning("LINKEDIN FAIL: {Message}", res.Error.ReasonText);
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
