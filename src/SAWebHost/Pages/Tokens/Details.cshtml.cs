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
    public class DetailsModel : PageModel
    {
        private readonly SAWebHost.Data.TokenRegistryContext _context;

        public DetailsModel(SAWebHost.Data.TokenRegistryContext context)
        {
            _context = context;
        }

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
    }
}
