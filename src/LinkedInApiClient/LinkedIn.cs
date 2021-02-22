using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.AccessControl;
using LinkedInApiClient.UseCases.CareerPageStatistics;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Standardized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public static class LinkedIn
    {
        public static FindAMembersOrganizationAccessControlInformation FindAMembersOrganizationAccessControlInformation(string tokenId)
        {
            return new FindAMembersOrganizationAccessControlInformation(tokenId);
        }

        public static FindOrganizationAdministrators FindOrganizationAdministrators(string tokenId, LinkedInURN organizationUrn)
        {
            return new FindOrganizationAdministrators(tokenId, organizationUrn);
        }

        public static RetrieveOrganizationBrandPageStatistics RetrieveOrganizationBrandPageStatistics(LinkedInURN organizationBrand, TimeInterval timeInterval, string tokenId)
        {
            return new RetrieveOrganizationBrandPageStatistics(organizationBrand, timeInterval, tokenId);
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
