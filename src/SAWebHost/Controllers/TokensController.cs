using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("JwtBearer")]
    public class TokensController : ControllerBase
    {
        private readonly SAWebHostContext _context;

        public TokensController(SAWebHostContext context)
        {
            _context = context;
        }

        // GET: api/Tokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessTokenEntry>>> GetAccessTokenEntry()
        {
            return await _context.AccessTokenEntry.ToListAsync();
        }

        // GET: api/Tokens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessTokenEntry>> GetAccessTokenEntry(string id)
        {
            var accessTokenEntry = await _context.AccessTokenEntry.FindAsync(id);

            if (accessTokenEntry == null)
            {
                return NotFound();
            }

            return accessTokenEntry;
        }

        // PUT: api/Tokens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessTokenEntry(string id, AccessTokenEntry accessTokenEntry)
        {
            if (id != accessTokenEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(accessTokenEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessTokenEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tokens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccessTokenEntry>> PostAccessTokenEntry(AccessTokenEntry accessTokenEntry)
        {
            _context.AccessTokenEntry.Add(accessTokenEntry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccessTokenEntryExists(accessTokenEntry.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccessTokenEntry", new { id = accessTokenEntry.Id }, accessTokenEntry);
        }

        // DELETE: api/Tokens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessTokenEntry(string id)
        {
            var accessTokenEntry = await _context.AccessTokenEntry.FindAsync(id);
            if (accessTokenEntry == null)
            {
                return NotFound();
            }

            _context.AccessTokenEntry.Remove(accessTokenEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessTokenEntryExists(string id)
        {
            return _context.AccessTokenEntry.Any(e => e.Id == id);
        }
    }
}
