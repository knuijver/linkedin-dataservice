using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Models
{
    public class OrganizationRoleEntry
    {
        [JsonPropertyName("roleAssignee")]
        public LinkedInURN RoleAssigneeUrn { get; set; }

        [JsonPropertyName("roleAssignee~")]
        public AssigneePerson RoleAssignee { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("organization")]
        public LinkedInURN OrganizationUrn { get; set; }

        [JsonPropertyName("organization~")]
        public OrganizationInfo Organization { get; set; }
    }
}
