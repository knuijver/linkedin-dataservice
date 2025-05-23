﻿using LinkedInApiClient.UseCases.AccessControl;
using LinkedInApiClient.UseCases.CareerPageStatistics;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Organizations;
using LinkedInApiClient.UseCases.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientAccessControlExtensions
    {
        public static Task<Result<LinkedInError, Paged<OrganizationRoleEntry>>> FindAMembersOrganizationAccessControlInformationAsync(
            this HttpMessageInvoker client,
            FindAMembersOrganizationAccessControlInformationRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

            return client.GetAsync<Paged<OrganizationRoleEntry>>(request.WithAccessToken(accessToken), cancellationToken);
        }

        public static Task<Result<LinkedInError, Paged<OrganizationRoleEntry>>> FindOrganizationAdministratorsAsync(
            this HttpMessageInvoker client,
            FindOrganizationAdministratorsRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

            return client.GetAsync<Paged<OrganizationRoleEntry>>(request.WithAccessToken(accessToken), cancellationToken);
        }
    }
}
