using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.AccessControl
{
    public class AssigneePerson
    {
        [JsonPropertyName("localizedFirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("localizedLastName")]
        public string LastName { get; set; }
    }
}
