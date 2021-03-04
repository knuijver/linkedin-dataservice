using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.Messages
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/ugc-post-api#find-ugc-posts-by-container-entities
    /// </summary>
    public class FindUGCPostsByContainerEntitiesRequest : LinkedInRequest
    {
        public FindUGCPostsByContainerEntitiesRequest(params LinkedInURN[] groupUrn)
        {
            var invalid = groupUrn
                .Where(urn => string.Equals("group", urn.EntityType, StringComparison.OrdinalIgnoreCase))
                .Select(urn => new ArgumentException($"{urn} is an invalid URN Type (personUrn|organizationUrn)", nameof(groupUrn)))
                .ToArray();

            if (invalid.Length > 0) throw new AggregateException($"{nameof(groupUrn)} contains invalid arguments", invalid);

            var urnList = string.Join(",", groupUrn.Select(s => s.UrlEncode()));


            Address = "ugcPosts";
            QueryParameters = new Parameters
            {
                ["q"] = "containerEntities",
                ["containerEntities"] = $"LIST({urnList})"
            };
        }
    }
}
