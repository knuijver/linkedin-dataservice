using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInApiClient.Types;
using Newtonsoft.Json;

namespace SAWebHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var model = new
            {
                Sample = CommonURN.OrganizationId("32523")
            };

            _logger.LogInformation(JsonConvert.SerializeObject(model, Formatting.Indented));
        }
    }
}
