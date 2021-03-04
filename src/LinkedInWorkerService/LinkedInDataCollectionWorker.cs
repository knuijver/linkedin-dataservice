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
using LinkedInApiClient.UseCases.Organizations;
using LinkedInApiClient.UseCases.People;
using LinkedInApiClient.Messages;

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
        private readonly ILogger<LinkedInDataCollectionWorker> logger;
        private readonly IAccessTokenRegistry tokenRegistry;

        public LinkedInDataCollectionWorker(ILogger<LinkedInDataCollectionWorker> logger, IAccessTokenRegistry tokenRegistry)
        {
            this.tokenRegistry = tokenRegistry;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new HttpClient()
                .UseDefaultLinkedInBaseUrl();

            var accessTokens = await tokenRegistry.ListAsync(CancellationToken.None);
            if (accessTokens.Try(out var tokens))
            {
                foreach (var token in tokens)
                {
                    var accessToken = token.AccessToken;

                    var me = await client.GetMyProfileAsync(new GetMyProfileRequest(), accessToken, stoppingToken);
                    if (!me.IsError)
                    {
                        logger.LogInformation("Collecting for {Person}", me.Raw);
                    }

                    var organizations = await client.FindAMembersOrganizationAccessControlInformationAsync(
                        new FindAMembersOrganizationAccessControlInformationRequest(), accessToken, stoppingToken);

                    if (organizations.Try(out var paged))
                    {
                        foreach (var org in paged.Elements)
                        {
                            logger.LogInformation("{OrganizationName} - {Urn} with {Role}",
                                org.Organization.Name,
                                org.OrganizationUrn,
                                org.Role);

                            var orginizationUrn = org.OrganizationUrn;

                            var followerStatistics = await client.RetrieveLifetimeFollowerStatisticsAsync(
                                new RetrieveLifetimeFollowerStatisticsRequest(orginizationUrn, default), accessToken, stoppingToken);

                            var pageStatistics = await client.RetrieveLifetimeOrganizationPageStatisticsAsync(
                                new RetrieveLifetimeOrganizationPageStatisticsRequest(orginizationUrn, default), accessToken, stoppingToken);

                            var posts = await client.FindUGCPostsByAuthorsAsync(
                                new FindUGCPostsByAuthorsRequest(orginizationUrn), accessToken, stoppingToken);

                            var socialActions = await client.BatchGetASummaryOfSocialActionsAsync(
                                new BatchGetASummaryOfSocialActionsRequest(), accessToken, stoppingToken);
                        }
                    }
                    else
                    {
                        logger.LogWarning("LINKEDIN FAIL: {Message}", organizations.Error.ReasonText);
                    }
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
