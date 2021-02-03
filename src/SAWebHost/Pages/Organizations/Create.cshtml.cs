using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Organizations
{
    public record TableOption
    {
        public int MyProperty { get; set; }
    }

    public class CreateModel : PageModel
    {
        private readonly SAWebHost.Data.SAWebHostContext _context;

        public CreateModel(SAWebHost.Data.SAWebHostContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Organization = new Organization
            {
                Id = IdGenerator.GetOne("organization", Guid.NewGuid())
            };

            return Page();
        }

        [BindProperty]
        public Organization Organization { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Organization.Add(Organization);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
