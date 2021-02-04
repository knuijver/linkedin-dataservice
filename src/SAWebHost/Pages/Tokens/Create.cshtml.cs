using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using LinkedInApiClient.Authentication;
using LinkedInApiClient.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SAWebHost.Data;
using SAWebHost.Data.Dto;

namespace SAWebHost.Pages.Tokens
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SAWebHostContext context;
        private readonly LinkedInOptions options;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateModel(SAWebHostContext context, IOptionsMonitor<LinkedInOptions> options, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.options = options.CurrentValue;
            this.context = context;
        }

        public async Task<IActionResult> OnGetAsync(string code = null, string state = null)
        {
            var user = await userManager.GetUserAsync(User);

            ApplicationOptions = new SelectList(context.LinkedInProvider, nameof(LinkedInProvider.Id), nameof(LinkedInProvider.ApplicationName));
            OrganizationOptions = new SelectList(context.Organization, nameof(Organization.Id), nameof(Organization.Name));

            if (!string.IsNullOrEmpty(code))
            {
                var client = new HttpClient();

                LinkedInProvider provider = GetProvider(state);

                var response = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
                {
                    Address = provider.TokenEndpoint,
                    ClientId = provider.ClientId,
                    ClientSecret = provider.ClientSecret,
                    RedirectUri = Url.PageLink("/Tokens/Create"),
                    Resource = provider.Scope.Split(" "),
                    Code = code
                });

                AccessTokenEntry = new AccessTokenEntry
                {
                    Id = IdGenerator.GetOne("token", Guid.NewGuid()),
                    OrganizationId = user.OrganizationUrn,
                    ProviderId = provider.Id,
                    CreatedOn = DateTimeOffset.Now,
                    AccessToken = response.AccessToken,
                    ExpiresIn = response.ExpiresIn,
                    RefreshToken = response.RefreshToken,
                    RefreshTokenExpiresIn = response.Json.GetProperty("refresh_token_expires_in").GetInt32()
                };
            }
            else
            {
                AccessTokenEntry = new AccessTokenEntry
                {
                    Id = IdGenerator.GetOne("token", Guid.NewGuid()),
                    CreatedOn = DateTimeOffset.Now,
                    OrganizationId = user.OrganizationUrn,
                };
            }

            return Page();
        }

        private LinkedInProvider GetProvider(string state)
        {
            if (!string.IsNullOrEmpty(state))
            {
                var providerId = LinkedInURN.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(state)));
                return context.LinkedInProvider.Find(providerId.ToString());
            }

            return null;
        }

        [BindProperty]
        public AccessTokenEntry AccessTokenEntry { get; set; }

        public SelectList ApplicationOptions { get; set; }

        public SelectList OrganizationOptions { get; set; }


        public async Task<IActionResult> OnPostConnectAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var provider = await context.LinkedInProvider.FindAsync(AccessTokenEntry.ProviderId);
            var query = new QueryParameterCollection
            {
                ["response_type"] = "code",
                ["client_id"] = provider.ClientId,
                ["redirect_uri"] = Url.PageLink("/Tokens/Create"),
                ["state"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(provider.Id)),
                ["scope"] = provider.Scope
            };

            return Redirect(query.ToUrlQueryString(provider.AuthorizationEndpoint));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            context.AccessTokenEntry.Add(AccessTokenEntry);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
