using LinkedInApiClient.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAWebHost.Controllers.LinkedIn
{
    [Route("api/linkedin/connect")]
    [ApiController]
    public class ConnectController : ControllerBase
    {
        readonly LinkedInHandler handler;
        readonly IOptionsMonitor<LinkedInOptions> options;

        public ConnectController(IOptionsMonitor<LinkedInOptions> options, LinkedInHandler handler)
        {
            this.options = options;
            this.handler = handler;
        }

        public IActionResult Get()
        {
            return Ok();
        }
    }
}
