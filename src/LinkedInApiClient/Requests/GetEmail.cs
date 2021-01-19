using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    public class GetEmail : LinkedInRequest
    {
        public GetEmail()
            : base("emailAddress", new Dictionary<string, string>
            {
                ["q"] = "members",
                ["projection"] = "(elements*(handle~))"
            })
        {
        }
    }
}
