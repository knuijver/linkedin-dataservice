using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient;
using LinkedInApiClient.Extensions;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.AccessControl;
using LinkedInApiClient.UseCases.CareerPageStatistics;
using LinkedInApiClient.UseCases.Organizations;
using LinkedInApiClient.UseCases.People;
using LinkedInApiClient.UseCases.Shares;
using LinkedInApiClient.UseCases.Social;
using LinkedInApiClient.UseCases.Standardized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedInApiClientTests
{
    [TestClass]
    [TestCategory("Integration Tests")]
    [Ignore("Only run in dev env. may require new AccessTokens")]
    public class LinkedInApi_IntegrationTests
    {
        [TestMethod]
        public void DefaultTokenEndpoint_IsAbsoluteUrl()
        {
            if (!Uri.TryCreate(LinkedInConstants.DefaultTokenEndpoint, UriKind.Absolute, out var uri))
            {
                Assert.Fail($"{nameof(LinkedInConstants.DefaultTokenEndpoint)} is not defined as an Absolute Url");
            }
        }

        private async Task<(HttpClient, string)> Connection()
        {
            var tokenRegistry = DummyTokenRegistry.Create();
            var client = new HttpClient()
                .UseDefaultLinkedInBaseUrl();

            var tokenResponse = await tokenRegistry.AccessTokenAsync(DummyTokenRegistry.ValidTokenId, default);
            if (!tokenResponse.IsSuccess) Assert.Fail(tokenResponse.Error.ReasonText);

            return (client, tokenResponse.Data);
        }

        [TestMethod]
        public async Task RetrieveAllFunctions()
        {
            var (client, accessToken) = await Connection();

            var result = await client.GetAllFunctionsAsync(
                new GetAllFunctionsRequest(Locale.From(new CultureInfo("nl-NL")).ToString())
                    , accessToken);


            if (!result.IsSuccess) Assert.Fail(result.Error.ReasonText);

            var function = result.Data.Elements[^5];
            string functionName = function.Name;
            Console.WriteLine($"URN: {function.FunctionURN} = {functionName}");
        }

        [TestMethod]
        public async Task RetrieveAllCountries()
        {
            var (client, accessToken) = await Connection();

            var result = await client.GetAllCountriesAsync(new GetAllCountriesRequest(Locale.Default), accessToken);

            if (!result.IsSuccess) Assert.Fail(result.Error.ReasonText);
        }

        [TestMethod]
        public async Task RetrieveOrganizationBrandPageStatistics()
        {
            var (client, accessToken) = await Connection();
            var result = await client.RetrieveOrganizationBrandPageStatisticsAsync(
                new RetrieveOrganizationBrandPageStatisticsRequest(CommonURN.OrganizationBrand("72216557"), default)
                    , accessToken);

            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task EmailAddress()
        {
            var (client, accessToken) = await Connection();

            var result = await client.GetEmailAsync(new GetEmailRequest(), accessToken);

            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task Profile()
        {
            var (client, accessToken) = await Connection();

            var result = await client.GetMyProfileAsync(new GetMyProfileRequest(), accessToken);

            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task FindOrganizationByEmailDomain()
        {
            var (client, accessToken) = await Connection();

            var result = await client.FindOrganizationByEmailDomainRequestAsync(
                new FindOrganizationByEmailDomainRequest("tasper.nl")
                    , accessToken);

            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task FindOrganizationByVanityName()
        {
            var (client, accessToken) = await Connection();

            var result = await client.FindOrganizationByVanityNameAsync(
                new FindOrganizationByVanityNameRequest("Fantistics")
                    , accessToken);

            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task OrganizationShares()
        {
            var (client, accessToken) = await Connection();

            var result = await client.OrganizationSharesAsync(
                new OrganizationSharesRequest(CommonURN.OrganizationId("37246747"))
                , accessToken);

            if (!result.IsSuccess) Assert.Fail(result.Error.ReasonText);
        }

        [TestMethod]
        public async Task RetrieveLikesOnShares()
        {
            var (client, accessToken) = await Connection();

            var result = await client.RetrieveLikesOnSharesAsync(
                new RetrieveLikesOnSharesRequest(CommonURN.Share("6762019588700987393"))
                    , accessToken);

            if (!result.IsSuccess) Assert.Fail(result.Error.ReasonText);
        }


        [TestMethod]
        public async Task RetrieveAnAdministeredOrganization()
        {
            var (client, accessToken) = await Connection();

            var result = await client.RetrieveAnAdministeredOrganizationAsync(
                new RetrieveAnAdministeredOrganizationRequest(CommonURN.OrganizationId("72216557"))
                    , accessToken);

            Assert.AreEqual(HttpStatusCode.Forbidden, result.HttpStatusCode);
            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task RetrieveOrganizationFollowerCount()
        {
            var (client, accessToken) = await Connection();

            var result = await client.RetrieveOrganizationFollowerCountAsync(
                new RetrieveOrganizationFollowerCountRequest(CommonURN.OrganizationId("37246747"))
                    , accessToken);

            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod, Obsolete]
        public async Task ActivityFeedNetworkShares()
        {
            var (client, accessToken) = await Connection();

            var result = await client.ExecuteRequest(new ActivityFeedNetworkSharesRequest(
                LinkedInURN.None,
                LinkedInURN.None,
                null
                ).WithAccessToken(accessToken));

            Assert.AreEqual(HttpStatusCode.Unauthorized, result.HttpStatusCode);
            //if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task LookUpShareById()
        {
            var (client, accessToken) = await Connection();

            var result = await client.LookUpShareByIdAsync(
                new LookUpShareByIdRequest(CommonURN.Share("123"))
                    , accessToken);


            if (!result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task RetrieveLifetimeFollowerStatistics()
        {
            var (client, accessToken) = await Connection();

            var result = await client.RetrieveLifetimeFollowerStatisticsAsync(
                new RetrieveLifetimeFollowerStatisticsRequest(CommonURN.OrganizationId("72216557"), default)
                    , accessToken);

            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task RetrieveLifetimeOrganizationPageStatistics()
        {
            var (client, accessToken) = await Connection();

            var result = await client.RetrieveLifetimeOrganizationPageStatisticsAsync(
                new RetrieveLifetimeOrganizationPageStatisticsRequest(
                //CommonURN.OrganizationId("72216557"),
                CommonURN.OrganizationId("37246747"),
                default
                ), accessToken);


            if (result.IsError) Assert.Fail(result.Error);
        }

        [TestMethod]
        public async Task FindAMembersOrganizationAccessControlInformation()
        {
            var (client, accessToken) = await Connection();

            var result = await client.FindAMembersOrganizationAccessControlInformationAsync(
                new FindAMembersOrganizationAccessControlInformationRequest(), accessToken);

            if (!result.IsSuccess) Assert.Fail(result.Error.ReasonText);
        }

        [TestMethod]
        public async Task FindOrganizationAdministrators()
        {
            var (client, accessToken) = await Connection();

            var result = await client.FindOrganizationAdministratorsAsync(
                new FindOrganizationAdministratorsRequest(
                //CommonURN.OrganizationId("45271")
                CommonURN.OrganizationId("37246747")
                ), accessToken);

            if (!result.IsSuccess)
            {
                var failure = (result.Error) switch
                {
                    LinkedInHttpError error when error.StatusCode == HttpStatusCode.Unauthorized => "Malformed requests. Typically, the Access Control fields are invalid.",
                    LinkedInHttpError error when error.StatusCode == HttpStatusCode.Forbidden => "A viewer is not present, or the user is not authorized to modify the Access Control.",
                    LinkedInHttpError error when error.StatusCode == HttpStatusCode.NotFound => "The Access Control does not exist.",
                    _ => result.Error.ReasonText
                };

                Assert.Fail(failure);
            }
        }


        [TestMethod]
        public async Task ResponseTypeChecking()
        {
            var (client, accessToken) = await Connection();
            var req = new LinkedInRequest
            {
                AccessToken = "HelloWorld",
                Address = "test"
            };

            await client.SendAsync(req);

        }

        [TestMethod]
        public async Task ClinetAuthRequest()
        {
            var req = await GetApiDataUsingHttpClientHandler();
        }

        private async Task<JsonDocument> GetApiDataUsingHttpClientHandler()
        {
            var cert = new X509Certificate2(Path.Combine("D:\\Certificates\\", "SAWebHost.pfx"), "1234");
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);
            var client = new HttpClient(handler);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://localhost:5011/api/fans/urn:fan:token:ad2c9a3e"),
                Method = HttpMethod.Get,
            };
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonDocument.Parse(responseContent);
                return data;
            }

            throw new ApplicationException($"Status code: {response.StatusCode}, Error: {response.ReasonPhrase}");
        }
    }
}
