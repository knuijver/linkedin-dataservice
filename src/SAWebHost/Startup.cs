using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SAWebHost.Data;
using SAWebHost.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SAWebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<SAWebHostContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthentication()
                .AddCookie()
                //.AddIdentityServerAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtAuth:Issuer"],
                        ValidAudience = Configuration["JwtAuth:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuth:Secret"]))
                    };
                });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("JwtBearer", policy =>
            //    {
            //        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
            //    });
            //});

            //CertificateAuthentication(services);

            /*
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = "LinkedIn";
            })
            .AddCookie()
            .AddLinkedIn("LinkedIn", "LinkedIn", options =>
            {
                options.ClientId = "86po09bhnplgei";
                options.ClientSecret = "F9PalSPEg6ZHP1ao";
                //options.SaveTokens = true;
            });
            */

            services.AddSingleton<IBase64QrCodeGenerator, Base64QrCodeGenerator>();
            services.AddSingleton<ICertificateValidationService, StoreCertificateValidationService>();
        }

        private static void CertificateAuthentication(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddCertificate(options =>
                {
                    options.AllowedCertificateTypes = CertificateTypes.All;
                    options.Events = new CertificateAuthenticationEvents
                    {
                        OnCertificateValidated = context =>
                        {
                            var validationService = context.HttpContext.RequestServices.GetRequiredService<ICertificateValidationService>();
                            if (validationService.ValidateCertificate(context.ClientCertificate))
                            {
                                var claims = new[]
                                {
                                    new Claim(
                                        ClaimTypes.NameIdentifier,
                                        context.ClientCertificate.Subject,
                                        ClaimValueTypes.String,
                                        context.Options.ClaimsIssuer
                                        ),
                                    new Claim(
                                        ClaimTypes.Name,
                                        context.ClientCertificate.Subject,
                                        ClaimValueTypes.String,
                                        context.Options.ClaimsIssuer
                                        )
                                };

                                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));

                                context.Success();
                            }
                            else
                            {
                                context.Fail("invalid certificate");
                            }
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.Fail("invalid certificate");
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddCertificateCache();

            services.AddCertificateForwarding(options =>
            {
                options.CertificateHeader = "X-CLIENT-CERT";
                options.HeaderConverter = (headerValue) =>
                {
                    X509Certificate2 clientCertificate = null;

                    if (!string.IsNullOrWhiteSpace(headerValue))
                    {
                        byte[] bytes = StringToByteArray(headerValue);
                        clientCertificate = new X509Certificate2(bytes);
                    }

                    return clientCertificate;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCertificateForwarding();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }

        private static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}
