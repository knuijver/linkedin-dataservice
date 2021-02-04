using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Applications
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly SAWebHost.Data.SAWebHostContext _context;

        public EditModel(SAWebHost.Data.SAWebHostContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LinkedInProvider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkedInProviderExists(LinkedInProvider.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LinkedInProviderExists(string id)
        {
            return _context.LinkedInProvider.Any(e => e.Id == id);
        }
    }
}
