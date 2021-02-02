using System;
using System.Net;
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedInApiClientTests
{
    [TestClass, TestCategory("Integration Tests")]
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
                CommonURN.Share("123"),
                QueryParameterCollection.EmptyParameters);

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
