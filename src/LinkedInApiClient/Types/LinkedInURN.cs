using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.Types
{
    [Serializable]
    public class LinkedInURN : ISerializable
    {
        public LinkedInURN()
        {
        }
        public LinkedInURN(string @namespace, string entityType, string id)
        {
            Namespace = @namespace;
            EntityType = entityType;
            Id = id;
        }

        protected LinkedInURN(SerializationInfo info, StreamingContext context)
        {
            var urn = info.GetString("$urn");
            if (!string.IsNullOrWhiteSpace(urn))
            {
                var parts = urn.Split(":");
                if (parts[0] == "urn" && parts.Length >= 3)
                {
                    Namespace = parts[1];
                    EntityType = parts[2];
                    Id = parts[3];
                }
            }
        }

        public string @Namespace { get; private set; }

        public string EntityType { get; private set; }

        public string Id { get; private set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("$urn", this.ToString());
        }

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