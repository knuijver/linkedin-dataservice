using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Standardized.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#nullable enable

namespace LinkedInApiClient.UseCases.Standardized
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/shared/references/v2/standardized-data/functions#get_all
    /// </summary>
    public class GetAllFunctions : ILinkedInRequest<Paged<JobsFunction>>
    {
        public GetAllFunctions(string tokenId, string? localeString = null)
        {
            TokenId = tokenId;
            Url = "functions";
            QueryParameters = new Parameters
            {
                ["count"] = "100"
            };
            if (!string.IsNullOrWhiteSpace(localeString)) QueryParameters["locale"] = localeString;
        }

        public string TokenId { get; }
        public string Url { get; }
        public Parameters QueryParameters { get; }
    }
}
