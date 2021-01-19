using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    public class GetProfile : LinkedInRequest
    {
        public GetProfile()
            : base("me", new Dictionary<string, string>
            {
                ["projection"] = "(id,firstName,lastName,profilePicture(displayImage~:playableStreams))"
            })
        {
        }
    }
}
