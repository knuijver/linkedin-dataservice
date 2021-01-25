using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases;

namespace LinkedInApiClient
{
    public class LinkedInClient
    {
        private readonly IAccessTokenRegistry tokenRegistry;
        private readonly LinkedInApiHandler handler;

        public LinkedInClient(IAccessTokenRegistry tokenRegistry, LinkedInApiHandler handler)
        {
            this.handler = handler;
            this.tokenRegistry = tokenRegistry;
        }

        public async Task<Option<string>> EmailAddress(string tokenId)
        {
            var token = (await tokenRegistry.AccessTokenAsync(tokenId));
            if (token.IsSuccess)
            {
                return (await handler.Query(new AuthenticatedRequest(token.Data, new GetEmail())))
                    .GetOptionalResult();
            }
            else
            {
                return Option.None;
            }
        }

        public async Task<Option<string>> Profile(string tokenId)
        {
            var token = await tokenRegistry.AccessTokenAsync(tokenId);
            if (token.IsSuccess)
            {
                var result = await handler.Query(new AuthenticatedRequest(token.Data, new GetProfile()));
                return result.GetOptionalResult();
            }
            else
            {
                return Option.None;
            }
        }
    }
}
