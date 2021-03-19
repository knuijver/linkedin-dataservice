using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Organizations
{
    public class DetailsModel : PageModel
    {
        private readonly SAWebHost.Data.TokenRegistryContext _context;

        public DetailsModel(SAWebHost.Data.TokenRegistryContext context)
        {
            _context = context;
        }

        public Organization Organization { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Organization = await _context.Organization.FirstOrDefaultAsync(m => m.Id == id);

            if (Organization == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
