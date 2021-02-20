using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using LinkedInWorkerService.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LinkedInWorkerService.Common
{
    internal class StoreApiAuthenticationHandler : DelegatingHandler
    {
        private readonly ILogger<StoreApiAuthenticationHandler> logger;
        private readonly ITokenService tokenService;

        public StoreApiAuthenticationHandler(ILogger<StoreApiAuthenticationHandler> logger, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await tokenService.GetTokenAsync("read");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            request.Version = new Version(2, 0);
            request.VersionPolicy = HttpVersionPolicy.RequestVersionOrLower;

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
