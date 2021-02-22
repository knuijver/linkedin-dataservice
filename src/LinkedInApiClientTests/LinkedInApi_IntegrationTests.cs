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
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases;
using LinkedInApiClient.UseCases.AccessControl;
using LinkedInApiClient.UseCases.CareerPageStatistics;
using LinkedInApiClient.UseCases.EmailAddress;
using LinkedInApiClient.UseCases.Organizations;
using LinkedInApiClient.UseCases.Shares;
using LinkedInApiClient.UseCases.Social;
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

        [TestMethod]
        public async Task RetrieveAllFunctions()
        {
            var message = LinkedIn.Standardized.AllFunctions(
                DummyTokenRegistry.ValidTokenId, 
                Locale.From(new CultureInfo("nl-NL")));

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);

            var function = result.Data.Elements[^5];
            string functionName = function.Name;
            Console.WriteLine($"URN: {function.FunctionURN} = {functionName}");
        }

        [TestMethod]
        public async Task RetrieveAllCountries()
        {
            var message = LinkedIn.Standardized.GetAllCountries(DummyTokenRegistry.ValidTokenId, Locale.Default);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task RetrieveOrganizationBrandPageStatistics()
        {
            var message = new RetrieveOrganizationBrandPageStatistics(
                CommonURN.OrganizationBrand("72216557"),
                default,
                DummyTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task EmailAddress()
        {
            var message = new GetEmail(DummyTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task Profile()
        {
            var message = new GetProfile(DummyTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task FindOrganizationByEmailDomain()
        {
            var message = new FindOrganizationByEmailDomain(
                DummyTokenRegistry.ValidTokenId,
                "tasper.nl");

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task FindOrganizationByVanityName()
        {
            var message = new FindOrganizationByVanityName(
                DummyTokenRegistry.ValidTokenId,
                "Fantistics");

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task OrganizationShares()
        {
            var message = new OrganizationShares(
                DummyTokenRegistry.ValidTokenId,
                CommonURN.OrganizationId("37246747"));

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task RetrieveLikesOnShares()
        {
            var message = new RetrieveLikesOnShares(
                DummyTokenRegistry.ValidTokenId,
                CommonURN.Share("6762019588700987393")
                );

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }


        [TestMethod]
        public async Task RetrieveAnAdministeredOrganization()
        {
            var message = new RetrieveAnAdministeredOrganization(
                DummyTokenRegistry.ValidTokenId,
                CommonURN.OrganizationId("72216557"));

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task RetrieveOrganizationFollowerCount()
        {
            var message = new RetrieveOrganizationFollowerCount(
                DummyTokenRegistry.ValidTokenId,
                CommonURN.OrganizationId("37246747"));

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod, Obsolete]
        public async Task ActivityFeedNetworkShares()
        {
            var message = new ActivityFeedNetworkShares(
                DummyTokenRegistry.ValidTokenId,
                LinkedInURN.None,
                LinkedInURN.None,
                null
                );

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task LookUpShareById()
        {
            var message = new LookUpShareById(
                DummyTokenRegistry.ValidTokenId,
                CommonURN.Share("123"));

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task RetrieveLifetimeFollowerStatistics()
        {
            var message = new RetrieveLifetimeFollowerStatistics(
                DummyTokenRegistry.ValidTokenId,
                CommonURN.OrganizationId("72216557"),
                default);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task RetrieveLifetimeOrganizationPageStatistics()
        {
            var message = new RetrieveLifetimeOrganizationPageStatistics(
                DummyTokenRegistry.ValidTokenId,
                //CommonURN.OrganizationId("72216557"),
                CommonURN.OrganizationId("37246747"),
                default
                );

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task FindAMembersOrganizationAccessControlInformation()
        {
            var message = new FindAMembersOrganizationAccessControlInformation(DummyTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task FindOrganizationAdministrators()
        {
            var message = new FindOrganizationAdministrators(
                DummyTokenRegistry.ValidTokenId,
                //CommonURN.OrganizationId("45271")
                CommonURN.OrganizationId("37246747")
                );


            var result = await SendRequest(message);

            if (!result.IsSuccess)
            {
                var failure = (result.Error) switch
                {
                    LinkedInHttpError error when error.StatusCode == HttpStatusCode.Unauthorized => "Malformed requests. Typically, the Access Control fields are invalid.",
                    LinkedInHttpError error when error.StatusCode == HttpStatusCode.Forbidden => "A viewer is not present, or the user is not authorized to modify the Access Control.",
                    LinkedInHttpError error when error.StatusCode == HttpStatusCode.NotFound => "The Access Control does not exist.",
                    _ => result.Error.Message
                };

                Assert.Fail(failure);
            }
        }


        [TestMethod]
        public async Task ResponseTypeChecking()
        {
            var req = GenericApiQuery.Create<string>("HelloWorld", "test", QueryParameterCollection.EmptyParameters);
            await SendRequest(req);

        }

        [TestMethod]
        public async Task ClinetAuthRequest()
        {
            var req = await GetApiDataUsingHttpClientHandler();
        }

        public async Task<Result<LinkedInError, T>> SendRequest<T>(ILinkedInRequest<T> request)
        {
            request.Validate();

            var tokenRegistry = DummyTokenRegistry.Create();
            var client = new LinkedInHttpClient();

            var result = await request.Handle(tokenRegistry, client, CancellationToken.None);
            return result;
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
