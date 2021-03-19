using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SATokenRegisterApi.Data.Dto
{
    public class Organization
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
