using LinkedInApiClient;
using LinkedInApiClient.Store;
using LinkedInApiClient.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.AccessControl;
using LinkedInApiClient.UseCases.Organizations;
using LinkedInApiClient.UseCases.People;
using LinkedInApiClient.UseCases;
using LinkedInApiClient.Types;

namespace LinkedInWorkerService
{
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

            var accessTokens = await tokenRegistry.ListAsync(stoppingToken);
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
                            try
                            {
                                var today = DateTime.Today;
                                var offset = TimeSpan.FromHours(0); // UTC

                                var timeInterval = new TimeInterval
                                {
                                    TimeGranularityType = TimeGranularityType.Day,
                                    TimeRange = new TimeRange
                                    {
                                        Start = new DateTimeOffset(today.AddDays(-(today.Day - 1)), offset),
                                        End = new DateTimeOffset(today.AddDays(1), offset)
                                    }
                                };

                                var followerStatistics = await client.RetrieveLifetimeFollowerStatisticsAsync(
                                    new RetrieveLifetimeFollowerStatisticsRequest(orginizationUrn, timeInterval), accessToken, stoppingToken);

                                logger.LogInformation("{FollowerStatistcs}", followerStatistics.Raw);

                                var pageStatistics = await client.RetrieveLifetimeOrganizationPageStatisticsAsync(
                                    new RetrieveLifetimeOrganizationPageStatisticsRequest(orginizationUrn, timeInterval), accessToken, stoppingToken);

                                logger.LogInformation("{PageStatistics}", pageStatistics.Raw);

                                var posts = await client.FindUGCPostsByAuthorsAsync(
                                    new FindUGCPostsByAuthorsRequest(orginizationUrn), accessToken, stoppingToken);

                                logger.LogInformation("{@UGCPosts}", posts.Data);

                                if (posts.Try(out var postPages))
                                {
                                    var urns = postPages.Elements
                                        .Select(s => s.Id)
                                        .Take(10)
                                        .ToArray();

                                    if (urns.Length > 0)
                                    {

                                        var socialActions = await client.BatchGetASummaryOfSocialActionsAsync(
                                            new BatchGetASummaryOfSocialActionsRequest(urns), accessToken, stoppingToken);

                                        logger.LogInformation("{SocialActions}", socialActions.Raw);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.LogWarning(ex, "failed pulling organization data.");
                            }
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
