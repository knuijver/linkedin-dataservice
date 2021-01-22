using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkedInApiClient.Types;

namespace LinkedInApiClient
{
    public class LinkedInClient
    {
        private readonly IAccessTokenRegistry tokenRegistry;
        private readonly LinkedInWebApiHandler handler;

        public LinkedInClient(IAccessTokenRegistry tokenRegistry, LinkedInWebApiHandler handler)
        {
            this.handler = handler;
            this.tokenRegistry = tokenRegistry;
        }

        public async Task<Option<string>> EmailAddress(string organizationId)
        {
            var token = (await tokenRegistry.AccessTokenAsync(organizationId));
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

        public async Task<Option<string>> Profile(string organizationId)
        {
            var token = await tokenRegistry.AccessTokenAsync(organizationId);
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
