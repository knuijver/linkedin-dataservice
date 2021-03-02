using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInWorkerService.Common.Types
{
    /// <summary>
    /// Represents an URN string 'urn:{namespace}:{entity}:{id}'
    /// </summary>
    public struct URN : LinkedInApiClient.Types.IURN
    {
        public static readonly URN None = new URN();

        public URN(string @namespace, string entityType, string id)
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

        public static implicit operator string(URN urn) => urn.ToString();

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

        /// <summary>
        /// When parsing an URN string with an Invalid format it will, return a LinkedInURN with HasValue set to False.
        /// </summary>
        /// <param name="urn"></param>
        /// <returns></returns>
        public static URN Parse(string urn)
        {
            if (!string.IsNullOrWhiteSpace(urn))
            {
                var parts = new string[4];
                var count = 0;
                while (count <= parts.Length || urn.Length <= 0)
                {
                    var pos = urn.IndexOf(':');
                    if (pos > 0 && urn[0] != '(')
                    {
                        parts[count++] = urn.Substring(0, pos);
                        urn = urn.Substring(++pos);
                    }
                    else
                    {
                        parts[count++] = urn;
                        break;
                    }
                };

                if (parts[0] == "urn")
                {
                    return new URN(parts[1], parts[2], parts[3]);
                }
            }
            return new URN();
        }

        public static URN TokenStoreUrn(string entityType, string id)
            => new URN("fan", entityType, id);
    }
}
