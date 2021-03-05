using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class AssigneePerson
    {
        [JsonPropertyName("localizedFirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("localizedLastName")]
        public string LastName { get; set; }
    }
}
