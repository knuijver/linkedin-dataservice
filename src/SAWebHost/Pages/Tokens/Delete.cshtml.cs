using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Tokens
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly SAWebHost.Data.TokenRegistryContext _context;

        public DeleteModel(SAWebHost.Data.TokenRegistryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AccessTokenEntry AccessTokenEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccessTokenEntry = await _context.AccessTokenEntry.FirstOrDefaultAsync(m => m.Id == id);

            if (AccessTokenEntry == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccessTokenEntry = await _context.AccessTokenEntry.FindAsync(id);

            if (AccessTokenEntry != null)
            {
                _context.AccessTokenEntry.Remove(AccessTokenEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
