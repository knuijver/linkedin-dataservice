using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SAWebHost.Data;
using SAWebHost.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SAWebHost.Impl
{
    public interface ICertificateValidationService
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
    }
    internal class StoreCertificateValidationService : ICertificateValidationService
    {
        private readonly ILogger<StoreCertificateValidationService> logger;
        public StoreCertificateValidationService(ILogger<StoreCertificateValidationService> logger)
        {
            this.logger = logger;
        }

        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                var cert = store.Certificates.Find(X509FindType.FindByThumbprint, clientCertificate.Thumbprint, true);

                logger.LogInformation("Yes    {Count}  {Name}, {Location}", store.Certificates.Count, store.Name, store.Location);

                return (cert != null);
            }
            catch (CryptographicException ex)
            {
                logger.LogError(ex, "No           {Name}, {Location}", store.Name, store.Location);
            }
            return false;
        }
    }

    internal class ThumbprintRegisterCertificateValidation : ICertificateValidationService
    {
        private readonly ILogger<ThumbprintRegisterCertificateValidation> logger;
        private readonly IServiceScopeFactory scopeFactory;

        public ThumbprintRegisterCertificateValidation(ILogger<ThumbprintRegisterCertificateValidation> logger, IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            this.logger = logger;
        }

        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            using var scope = scopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TokenRegistryContext>();

            return context
                .Set<ValidThumbprint>()
                .Any(a => a.Thumbprint == clientCertificate.Thumbprint);
        }
    }
}
