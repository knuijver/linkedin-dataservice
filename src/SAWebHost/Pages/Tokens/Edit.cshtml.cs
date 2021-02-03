using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Tokens
{
    public class EditModel : PageModel
    {
        private readonly SAWebHost.Data.SAWebHostContext _context;

        public EditModel(SAWebHost.Data.SAWebHostContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AccessTokenEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessTokenEntryExists(AccessTokenEntry.Id))
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

        private bool AccessTokenEntryExists(string id)
        {
            return _context.AccessTokenEntry.Any(e => e.Id == id);
        }
    }
}
