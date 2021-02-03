using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Applications
{
    public class DeleteModel : PageModel
    {
        private readonly SAWebHost.Data.SAWebHostContext _context;

        public DeleteModel(SAWebHost.Data.SAWebHostContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LinkedInProvider LinkedInProvider { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LinkedInProvider = await _context.LinkedInProvider.FirstOrDefaultAsync(m => m.Id == id);

            if (LinkedInProvider == null)
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

            LinkedInProvider = await _context.LinkedInProvider.FindAsync(id);

            if (LinkedInProvider != null)
            {
                _context.LinkedInProvider.Remove(LinkedInProvider);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
