using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SATokenRegisterApi.Controllers
{
    [Authorize(Policy = "Viewer")]
    [ApiController]
    [Route("[controller]")]
    public class MeController : ControllerBase
    {
        private readonly ILogger<MeController> logger;

        public MeController(ILogger<MeController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(new
            {
                Claims = this.User?.Claims.Select(c => new
                {
                    c.Type,
                    c.Value,
                    c.Issuer
                }).ToArray()
            });
    }
}
