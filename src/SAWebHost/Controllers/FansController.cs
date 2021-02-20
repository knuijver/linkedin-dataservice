using LinkedInApiClient.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAWebHost.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SAWebHost.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize("JwtBearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class FansController : ControllerBase
    {
        private readonly SAWebHostContext context;

        public FansController(SAWebHostContext context)
        {
            this.context = context;
        }


        // GET api/<FansController>/5
        [HttpGet("{urn}")]
        public async Task<IActionResult> Get(string urn, CancellationToken ct)
        {
            var id = LinkedInURN.Parse(urn);
            if (id.HasValue && id.Namespace == "fan")
            {
                switch (id.EntityType)
                {
                    case "organization":
                        return Ok(await context.Organization.FindAsync(new[] { id.ToString() }, ct));

                    case "token":
                        return Ok(await context.AccessTokenEntry.FindAsync(new[] { id.ToString() }, ct));

                    case "app":
                        return Ok(await context.LinkedInProvider.FindAsync(new[] { id.ToString() }, ct));

                    default:
                        return NotFound(id);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
