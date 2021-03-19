using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInApiClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Applications
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SAWebHost.Data.TokenRegistryContext _context;

        public CreateModel(SAWebHost.Data.TokenRegistryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            LinkedInProvider = new LinkedInProvider
            {
                Id = IdGenerator.GetOne("app", Guid.NewGuid())
            };

            return Page();
        }

        [BindProperty]
        public LinkedInProvider LinkedInProvider { get; set; }


        public IActionResult OnPostUseLinkedInSettings()
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
            }

            LinkedInProvider = new LinkedInProvider
            {
                ApplicationName = LinkedInProvider.ApplicationName,
                Id = LinkedInProvider.Id,
                IsActive = LinkedInProvider.IsActive,
                ClientId = LinkedInProvider.ClientId,
                ClientSecret = LinkedInProvider.ClientSecret,
                AuthorizationEndpoint = LinkedInConstants.DefaultAuthorizationEndpoint,
                TokenEndpoint = LinkedInConstants.DefaultTokenEndpoint,
                Scope = "r_organization_social r_1st_connections_size r_ads_reporting rw_organization_admin r_basicprofile r_ads"
            };

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LinkedInProvider.Add(LinkedInProvider);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
