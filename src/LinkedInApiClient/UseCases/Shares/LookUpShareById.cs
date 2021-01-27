using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Shares
{
    public class LookUpShareById : ILinkedInRequest
    {
        public LookUpShareById(string tokenId, LinkedInURN shareId, QueryParameterCollection queryParameters)
        {
            TokenId = tokenId;
            Url = $"shares/{shareId}";
            QueryParameters = queryParameters;
        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }
}
