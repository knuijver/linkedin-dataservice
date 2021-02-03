using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly SAWebHostContext _context;

        public ProvidersController(SAWebHostContext context)
        {
            _context = context;
        }

        // GET: api/Providers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinkedInProvider>>> GetLinkedInProvider()
        {
            return await _context.LinkedInProvider.ToListAsync();
        }

        // GET: api/Providers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LinkedInProvider>> GetLinkedInProvider(string id)
        {
            var linkedInProvider = await _context.LinkedInProvider.FindAsync(id);
            if (linkedInProvider == null)
            {
                return NotFound();
            }

            return linkedInProvider;
        }

        // PUT: api/Providers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLinkedInProvider(string id, LinkedInProvider linkedInProvider)
        {
            if (id != linkedInProvider.Id)
            {
                return BadRequest();
            }

            _context.Entry(linkedInProvider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkedInProviderExists(id))
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

        // POST: api/Providers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LinkedInProvider>> PostLinkedInProvider(LinkedInProvider linkedInProvider)
        {
            _context.LinkedInProvider.Add(linkedInProvider);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LinkedInProviderExists(linkedInProvider.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLinkedInProvider", new { id = linkedInProvider.Id }, linkedInProvider);
        }

        // DELETE: api/Providers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLinkedInProvider(string id)
        {
            var linkedInProvider = await _context.LinkedInProvider.FindAsync(id);
            if (linkedInProvider == null)
            {
                return NotFound();
            }

            _context.LinkedInProvider.Remove(linkedInProvider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LinkedInProviderExists(string id)
        {
            return _context.LinkedInProvider.Any(e => e.Id == id);
        }
    }
}
