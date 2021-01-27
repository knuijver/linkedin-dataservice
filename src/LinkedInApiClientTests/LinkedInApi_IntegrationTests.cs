using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases;
using LinkedInApiClient.UseCases.CareerPageStatistics;
using LinkedInApiClient.UseCases.EmailAddress;
using LinkedInApiClient.UseCases.Organizations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedInApiClientTests
{
    [TestClass]
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
        public void URNEncoding()
        {
            var urn = CommonURN.OrganizationId("37246747");
            var encoded = urn.UrlEncode();

            Assert.AreEqual("urn%3Ali%3Aorganization%3A37246747", encoded);
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
        public async Task FindOrganizationByVanityName()
        {
            var message = new FindOrganizationByVanityName(DummyTokenRegistry.ValidTokenId, "Fantistics");
            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task RetrieveOrganizationBrandPageStatistics()
        {
            var message = new RetrieveOrganizationBrandPageStatistics(
                CommonURN.OrganizationBrand("37246747"), 
                default, 
                DummyTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        [TestMethod]
        public async Task RetrieveLifetimeFollowerStatistics()
        {
            var message = new RetrieveLifetimeFollowerStatistics(
                CommonURN.OrganizationId("37246747"),
                default,
                DummyTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            if (!result.IsSuccess) Assert.Fail(result.Error.Message);
        }

        public async Task<Result<LinkedInError, JsonElement>> SendRequest(ILinkedInRequest request)
        {
            request.Validate();

            var tokenRegistry = DummyTokenRegistry.Create();
            var client = new LinkedInHttpClient();

            var result = await request.Handle(tokenRegistry, client, CancellationToken.None);
            return result;
        }
        public async Task<Result<LinkedInError, T>> SendRequest<T>(ILinkedInRequest<T> request)
        {
            request.Validate();

            var tokenRegistry = DummyTokenRegistry.Create();
            var client = new LinkedInHttpClient();

            var result = await request.Handle(tokenRegistry, client, CancellationToken.None);
            return result;
        }
    }
}
