using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SATokenRegisterApi.Data.Dto
{
    public class LinkedInProvider
    {
        public string Id { get; set; }

        [Required]
        public string ApplicationName { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ClientSecret { get; set; }

        public bool IsActive { get; set; }

        public string Scope { get; set; }

        [Required]
        public string AuthorizationEndpoint { get; set; }

        [Required]
        public string TokenEndpoint { get; set; }
    }
}
