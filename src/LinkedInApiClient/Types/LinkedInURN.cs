using System;
using System.Runtime.Serialization;

namespace LinkedInApiClient.Types
{
    public struct LinkedInURN
    {
        public static readonly LinkedInURN None = new LinkedInURN();

        public LinkedInURN(string @namespace, string entityType, string id)
        {
            HasValue = true;
            Namespace = @namespace;
            EntityType = entityType;
            Id = id;
        }

        public bool HasValue { get; private set; }

        public string @Namespace { get; private set; }

        public string EntityType { get; private set; }

        public string Id { get; private set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("$urn", this.ToString());
        }

        public static implicit operator string(LinkedInURN urn) => urn.ToString();

        public override string ToString()
        {
            return HasValue
                ? $"urn:{Namespace}:{EntityType}:{Id}"
                : "(none)";
        }

        public void Deconstruct(out string ns, out string entityType, out string id)
        {
            ns = this.Namespace;
            entityType = this.EntityType;
            id = this.Id;
        }

        public static LinkedInURN Parse(string urn)
        {
            if (!string.IsNullOrWhiteSpace(urn))
            {
                var parts = urn.Split(":");
                if (parts[0] == "urn" && parts.Length >= 3)
                {
                    return new LinkedInURN(parts[1], parts[2], parts[3]);
                }
            }

            return new LinkedInURN();
        }
    }
}
/*
urn:li:like:({personUrn},{activityUrn})	
Like URN can be  translated here  using the V2 Activities API.
Social Action API

urn:li:organization:{id}	

*/