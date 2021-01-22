using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.Types
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
}
/*
urn:li:like:({personUrn},{activityUrn})	
Like URN can be  translated here  using the V2 Activities API.
Social Action API

urn:li:organization:{id}	

*/