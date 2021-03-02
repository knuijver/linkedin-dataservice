using LinkedInApiClient;
using LinkedInApiClient.Types;
using LinkedInWorkerService.Common.Types;
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

        public LinkedInDataCollectionWorker(ILogger<LinkedInDataCollectionWorker> logger, IAccessTokenRegistry tokenRegistry )
        {
            this.tokenRegistry = tokenRegistry;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = new LinkedInHttpClient();
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
                    //LinkedIn.People.MeAsync(client, token, LinkedIn.People.Me(token.Id), stoppingToken);
                    //var me = await client.GetAsync(token.AccessToken, LinkedIn.People.Me(token.Id), stoppingToken);
                    //if(me.Try(out var profile))
                    //{
                        
                    //}
                }
            }


            LinkedIn.AccessControl.FindOrganizationAdministrators(URN.TokenStoreUrn("token", "c6379d94"), CommonURN.OrganizationId(""));
            var req = LinkedIn.AccessControl.FindAMembersOrganizationAccessControlInformation("urn:fan:token:c6379d94");
            var res = await req.HandleAsync(tokenRegistry, client, stoppingToken);

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
