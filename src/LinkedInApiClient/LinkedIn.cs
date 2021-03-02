using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.AccessControl;
using LinkedInApiClient.UseCases.CareerPageStatistics;
using LinkedInApiClient.UseCases.Organizations;
using LinkedInApiClient.UseCases.People;
using LinkedInApiClient.UseCases.Standardized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public static class LinkedIn
    {
        public static class People
        {
            public static GetMyProfile Me(string tokenId)
            {
                return new GetMyProfile(tokenId);
            }

            public static Task<Result<LinkedInError, string>> MeAsync(LinkedInHttpClient client, IStoredToken token, GetMyProfile me, CancellationToken stoppingToken)
            {
                return client.GetAsync(token.AccessToken, me, stoppingToken);
            }
        }

        public static class AccessControl
        {
            public static FindAMembersOrganizationAccessControlInformationRequest FindAMembersOrganizationAccessControlInformation(string tokenId)
            {
                return new FindAMembersOrganizationAccessControlInformationRequest(tokenId);
            }

            public static FindOrganizationAdministrators FindOrganizationAdministrators(string tokenId, LinkedInURN organizationUrn)
            {
                return new FindOrganizationAdministrators(tokenId, organizationUrn);
            }

            public static RetrieveOrganizationBrandPageStatistics RetrieveOrganizationBrandPageStatistics(LinkedInURN organizationBrand, TimeInterval timeInterval, string tokenId)
            {
                return new RetrieveOrganizationBrandPageStatistics(organizationBrand, timeInterval, tokenId);
            }
        }

        public static class Organizations
        {
            public static FindOrganizationByEmailDomain FindOrganizationByEmailDomain(string tokenId, string emailDomain)
            {
                return new FindOrganizationByEmailDomain(tokenId, emailDomain);
            }
        }

        public static class Standardized
        {
            public static GetAllFunctions AllFunctions(string tokenId, Locale locale)
                => new GetAllFunctions(tokenId, locale.ToString());

            public static GetAllCountries GetAllCountries(string tokenId, Locale locale)
                => new GetAllCountries(tokenId, locale);

            public static GetAllCountryGroups GetAllCountryGroups(string tokenId, Locale locale)
                => new GetAllCountryGroups(tokenId, locale);

            public static GetAllSeniorities GetAllSeniorities(string tokenId, Locale locale)
                => new GetAllSeniorities(tokenId, locale);
        }
    }
}
