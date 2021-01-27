using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient;
using LinkedInApiClient.UseCases;
using LinkedInApiClient.UseCases.EmailAddress;
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
        public async Task EmailAddress()
        {
            var message = new GetEmail(DummyAccessTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public async Task Profile()
        {
            var message = new GetProfile(DummyAccessTokenRegistry.ValidTokenId);

            var result = await SendRequest(message);

            Assert.IsTrue(result.IsSuccess);
        }

        public async Task<Result<LinkedInError, JsonElement>> SendRequest(ILinkedInRequest request)
        {
            request.Validate();

            var tokenRegistry = DummyAccessTokenRegistry.Create();
            var client = new LinkedInHttpClient();

            var result = await request.Handle(tokenRegistry, client, CancellationToken.None);
            return result;
        }
        public async Task<Result<LinkedInError, T>> SendRequest<T>(ILinkedInRequest<T> request)
        {
            request.Validate();

            var tokenRegistry = DummyAccessTokenRegistry.Create();
            var client = new LinkedInHttpClient();

            var result = await request.Handle(tokenRegistry, client, CancellationToken.None);
            return result;
        }
    }
}
