using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Social
{
    public class RetrieveASummaryOfSocialActions : ILinkedInRequest
    {
        public RetrieveASummaryOfSocialActions(string tokenId, LinkedInURN owner)
        {

            TokenId = tokenId;
            Url = "socialActions";

        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }
}
