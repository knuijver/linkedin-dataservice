using System;
using System.Linq;

namespace LinkedInApiClient
{
    public class AuthenticatedRequest
    {
        public AuthenticatedRequest(string bearerToken, LinkedInRequest request)
        {
            BearerToken = bearerToken ?? throw new ArgumentNullException(nameof(bearerToken));
            Request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public string BearerToken { get; private set; }

        public LinkedInRequest Request { get; private set; }
    }
}
