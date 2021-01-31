using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.AccessControl
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

    public class AssigneePerson
    {
        [JsonPropertyName("localizedFirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("localizedLastName")]
        public string LastName { get; set; }
    }

    public class OrganizationInfo
    {
        [JsonPropertyName("localizedName")]
        public string Name { get; set; }
    }
}
