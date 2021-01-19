using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public struct LinkedInURN
    {
        public LinkedInURN(string @namespace, string entityType, string id)
        {
            Namespace = @namespace;
            EntityType = entityType;
            Id = id;
        }

        public string @Namespace { get; private set; }

        public string EntityType { get; private set; }

        public string Id { get; private set; }

        public override string ToString()
        {
            return $"urn:{Namespace}:{EntityType}:{Id}";
        }
    }

    /// <summary>
    /// Common URNs & Namespaces
    /// </summary>
    public static class CommonURN
    {
        private static LinkedInURN LIN(string entityType, string id) => new LinkedInURN("li", entityType, id);

        /// <summary>
        /// Person ID is unique to each application.
        /// <see cref="https://docs.microsoft.com/en-us/linkedin/shared/integrations/people/profile-api"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LinkedInURN PersonId(string id) => LIN("person", id);

        /// <summary>
        /// Share URN can be translated here.
        /// Share API <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/share-api#migrate-from-update-keys-to-share-urns"/>
        /// UGC Post API <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/ugc-post-api"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LinkedInURN Share(string id) => LIN("share", id);

        /// <summary>
        /// Will need to utilize newsArticle URN <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/articles-api#retrieve-article-comments-and-likes"/>(e.g. urn:li:article:{id}) to retrieve comments and likes in V2 Social Actions API.
        /// Article API <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/articles-api"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LinkedInURN Article(string id) => LIN("originalArticle", id);

        /// <summary>
        /// This URN type currently captures video posts and will include more types in the future.
        /// UGC Post API <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/ugc-post-api"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LinkedInURN UGCPost(string id) => LIN("ugcPost", id);

        /// <summary>
        /// Comment URN can be  translated here<see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/share-api#migrate-from-update-keys-to-share-urns"/>  using the V2 Activities API.
        /// Social Action API <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/network-update-social-actions"/>
        /// </summary>
        /// <param name="activityURN"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public static LinkedInURN Comment(string activityURN, string commentId) => LIN("comment", $"({activityURN},{commentId})");
    }


    public static class LinkedInURNExtensions
    {

    }
}
/*
urn:li:like:({personUrn},{activityUrn})	
Like URN can be  translated here  using the V2 Activities API.
Social Action API

urn:li:organization:{id}	
Company ID translates to Organization Urn via urn:li:organization:{companyID}. 
Company URN and Organization URN are interchangeable through its namespace (e.g. urn:li:company:1 can become urn:li:organization:1).
A School ID will migrate to an Organization URN with an ID translation (e.g. school ID 123 will be urn:li:organization:456).
Organization AP
*/