using System;
using System.Linq;

namespace LinkedInApiClient
{
    public class AuthenticatedRequest
    {
        public AuthenticatedRequest(string bearerToken, ILinkedInRequest request)
        {
            BearerToken = bearerToken ?? throw new ArgumentNullException(nameof(bearerToken));
            Request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public string BearerToken { get; private set; }

        public ILinkedInRequest Request { get; private set; }
    }
}
