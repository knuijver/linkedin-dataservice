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
    public class GetAllFunctionsRequest : LinkedInRequest //Paged<JobsFunction>
    {
        public GetAllFunctionsRequest(string? localeString = null)
        {
            Address = "functions";
            QueryParameters = new Parameters
            {
                ["count"] = "100"
            };
            if (!string.IsNullOrWhiteSpace(localeString)) QueryParameters["locale"] = localeString;
        }
    }
}
