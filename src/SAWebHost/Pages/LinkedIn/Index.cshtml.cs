using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using LinkedInApiClient;
using LinkedInApiClient.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace SAWebHost.Pages.LinkedIn
{
    public class IndexModel : PageModel
    {
        readonly IOptionsMonitor<LinkedInOptions> options;

        public IndexModel(IOptionsMonitor<LinkedInOptions> options)
        {
            this.options = options;
        }

        public class AuthorizationCodeRequest
        {
            public string ClientId { get; set; }

            public string RedirectUri { get; set; }

            public string State { get; set; }

            public string Scope { get; set; }
            public string Address { get; internal set; }
        }

        public AuthorizationCodeRequest Step1 { get; set; } = new AuthorizationCodeRequest();

        [BindProperty]
        public string AccessToken { get; set; }

        public string Scope { get; set; }
        public string ReturnUrl { get; set; }

        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            Step1 = new AuthorizationCodeRequest
            {
                Address = this.options.CurrentValue.AuthorizationEndpoint,
                RedirectUri = Url.PageLink("/LinkedIn/Index"),
                ClientId = this.options.CurrentValue.ClientId,
                Scope = string.Join(" ", this.options.CurrentValue.Scope),
                State = Guid.NewGuid().ToString("n")
            };

            base.OnPageHandlerSelected(context);
        }


        public async Task OnGetAsync(string code = null)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var client = new HttpClient();

                var response = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
                {
                    Address = this.options.CurrentValue.TokenEndpoint,
                    ClientId = this.options.CurrentValue.ClientId,
                    ClientSecret = this.options.CurrentValue.ClientSecret,
                    RedirectUri = Url.PageLink("/LinkedIn/Index"),
                    Resource = this.options.CurrentValue.Scope,
                    Code = code
                });

                AccessToken = response.AccessToken;
            }
        }

        public async Task<IActionResult> OnPostConnectAsync(string returnUrl = null)
        {
            var scope = new List<string>();
            scope.Add("r_organization_social");
            scope.Add("r_1st_connections_size");
            scope.Add("r_ads_reporting");
            scope.Add("rw_organization_admin");
            scope.Add("r_basicprofile");
            scope.Add("r_ads");

            var options = new TokenClientOptions
            {
                Address = LinkedInConstants.DefaultAuthorizationEndpoint,
                ClientId = "86po09bhnplgei",
                ClientSecret = "F9PalSPEg6ZHP1ao",
            };

            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = LinkedInConstants.DefaultAuthorizationEndpoint,
                ClientId = "86po09bhnplgei",
                ClientSecret = "F9PalSPEg6ZHP1ao",
                Scope = string.Join(" ", scope),
                GrantType = "code",
                Resource = scope
            });

            var response = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
            {
                Address = LinkedInConstants.DefaultTokenEndpoint,
                ClientId = "86po09bhnplgei",
                ClientSecret = "F9PalSPEg6ZHP1ao",
                RedirectUri = Url.PageLink("/LinkedIn/Index"),
                Resource = scope
            });

            return Page();
        }
    }
}
