using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases
{
    /// <summary>
    /// You can retrieve all UGC posts for a member or an organization.
    /// Use authors=List(person Urn) or authors=List(organization Urn) as the query parameter.
    /// 
    /// Even though authors is structured as an Array, you can currently only pass one value for authors.
    /// You will get a 400 Bad Request error with the message "Multi author finder not implemented, please make separate requests per author"
    /// if you pass more than one value.
    ///
    /// URNs included in the URL params must be URL encoded. For example, urn:li:organization:12345 would become urn%3Ali%3Aorganization%3A12345.
    /// Other parts of the params do not need to be encoded. Postman or similar API tools may not support these types of calls.
    /// Testing with curl is recommended if you encounter a 400 error with message Invalid query parameters passed to request.
    /// <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/ugc-post-api#find-ugc-posts-by-authors"/>
    /// </summary>
    public class FindUGCPostsByAuthorsRequest : LinkedInRequest
    {
        public FindUGCPostsByAuthorsRequest(params LinkedInURN[] authorUrn)
        {
            var invalid = authorUrn
                .Where(urn => !urn.IsEntityTypeOf("person", "organization"))
                .Select(urn => new ArgumentException($"{urn} is an invalid URN Type (personUrn|organizationUrn)", nameof(authorUrn)))
                .ToArray();

            if (invalid.Length > 0) throw new AggregateException($"{nameof(authorUrn)} contains invalid arguments", invalid);

            var urnList = string.Join(",", authorUrn.Select(s=>s.UrlEncode()));

            ProtocolVersion = RestLiProtocolVersion.V2;
            Address = "ugcPosts";
            QueryParameters = new Types.Parameters
            {
                ["q"] = "authors",
                ["authors"] = $"List({urnList})",
                ["sortBy"] = "LAST_MODIFIED",
                ["count"] = "100",
                DisableValueEcoding = true
            };
        }
    }
}
