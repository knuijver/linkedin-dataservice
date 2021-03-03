using LinkedInWorkerService.Common;
using LinkedInWorkerService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using LinkedInApiClient.Store;

namespace LinkedInWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static string BaseAddress { get; }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<TokenStoreSettings>(hostContext.Configuration.GetSection(TokenService.HttpClientName));
                    services.AddTransient<ITokenService, TokenService>();

                    services.AddTransient<StoreApiAuthenticationHandler>();
                    services.AddHttpClient(nameof(AccessTokenStore), client =>
                    {
                        client.BaseAddress = hostContext.Configuration.GetValue<Uri>("TokenStoreUrl");
                    })
                    .AddHttpMessageHandler<StoreApiAuthenticationHandler>();

                    services.AddTransient<IAccessTokenRegistry, AccessTokenStore>();

                    services.AddHostedService<InboundMessageWorker>();
                    services.AddHostedService<LinkedInDataCollectionWorker>();
                    services.AddHostedService<LinkedInDataCollectionWorker>();
                });
    }
}
