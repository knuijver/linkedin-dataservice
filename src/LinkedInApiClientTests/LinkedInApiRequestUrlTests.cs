using LinkedInApiClient;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.EmailAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClientTests
{

    [TestClass]
    public class LinkedInApi_RequestMessageTests
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
        public void URNEncoding_For_RestLi_V2()
        {
            var urn = CommonURN.OrganizationId("37246747");
            var encoded = urn.UrlEncode();

            Assert.AreEqual("urn%3Ali%3Aorganization%3A37246747", encoded);
        }

        [TestMethod]
        public void UrnParsing_StringMustStartWith_urn()
        {
            var urn = LinkedInURN.Parse("urn:li:share:384576");

            Assert.IsTrue(urn.HasValue);

            Assert.AreEqual("li", urn.Namespace);
            Assert.AreEqual("share", urn.EntityType);
            Assert.AreEqual("384576", urn.Id);
        }

        [TestMethod]
        public void UrnParsing_CanExcept_NestedIdSegment()
        {
            var urn = LinkedInURN.Parse("urn:li:like:(urn:li:person:y635rRy2m3,urn:li:activity:6762019589283995648)");

            Assert.IsTrue(urn.HasValue);

            Assert.AreEqual("li", urn.Namespace);
            Assert.AreEqual("like", urn.EntityType);
            Assert.AreEqual("(urn:li:person:y635rRy2m3,urn:li:activity:6762019589283995648)", urn.Id);
        }

        [TestMethod]
        public void UrnParsing_CanExtract_References()
        {
            var urn = LinkedInURN.Parse("urn:li:like:(urn:li:person:y635rRy2m3,urn:li:activity:6762019589283995648)");

            Assert.IsTrue(urn.HasReferences());

            var refs = urn.IdReferences().ToArray();

            Assert.AreEqual(2, refs.Length);
        }

        [TestMethod]
        public async Task IEJ()
        {
            
            var result = await LinkedIn.FindOrganizationAdministrators(
                DummyTokenRegistry.ValidTokenId,
                CommonURN.OrganizationId("334345345"))
                .Handle(DummyTokenRegistry.Create(), new LinkedInHttpClient(), CancellationToken.None);

            result
                .IfHttpError(err =>
                {

                })
                .IfTokenFailure(err =>
                {

                })
                .IfException(err =>
                {

                });

            if (result.IsSuccess)
            {
            }
        }

        [TestMethod]
        public async Task RequestAccessToken_Generate_an_Access_Token()
        {
            Mock<HttpMessageHandler> handlerMock = Fakes.HttpMessageHandler(responseContent: new AccessTokenResponse());
            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var clientId = Guid.NewGuid().ToString("n");
            var secret = Convert.ToBase64String(Encoding.UTF8.GetBytes("Keep the Secret"));

            var uri = new Uri(LinkedInConstants.DefaultTokenEndpoint);
            var result = await linkedIn.RequestAccessToken(uri, clientId, secret, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);

            Fakes.VerifyRequest(
                handlerMock,
                req => req.Method == HttpMethod.Post
                    && req.RequestUri == uri
                    && req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded"
                );
        }

        [TestMethod]
        public async Task RequestAccessToken_HandlingInvalidTokens_401_Unauthorized()
        {
            Mock<HttpMessageHandler> handlerMock = Fakes.HttpMessageHandler(HttpStatusCode.Unauthorized, new ErrorResponse { Status = (int)HttpStatusCode.Unauthorized });
            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var clientId = Guid.NewGuid().ToString("n");
            var secret = Convert.ToBase64String(Encoding.UTF8.GetBytes("Keep the Secret"));

            var uri = new Uri(LinkedInConstants.DefaultTokenEndpoint);
            var result = await linkedIn.RequestAccessToken(uri, clientId, secret, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.IsInstanceOfType(result.Error, typeof(LinkedInHttpError));
            Assert.AreEqual(HttpStatusCode.Unauthorized, ((LinkedInHttpError)result.Error).StatusCode);
            Assert.IsNull(result.Data);

            Fakes.VerifyRequest(
                handlerMock,
                req => req.Method == HttpMethod.Post
                    && req.RequestUri == uri
                    && req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded"
                );
        }

        [TestMethod]
        public async Task Refreshing_a_Token()
        {
            Mock<HttpMessageHandler> handlerMock = Fakes.HttpMessageHandler(responseContent: DummyTokenRegistry.RefreshToken);
            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var clientId = Guid.NewGuid().ToString("n");
            var secret = Convert.ToBase64String(Encoding.UTF8.GetBytes("Keep the Secret"));
            var refreshToken = Convert.ToBase64String(Encoding.UTF8.GetBytes("Refresh"));

            var uri = new Uri(LinkedInConstants.DefaultTokenEndpoint);
            var result = await linkedIn.RefreshAccessToken(uri, clientId, secret, refreshToken, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);

            Fakes.VerifyRequest(
                handlerMock,
                req => req.Method == HttpMethod.Post
                    && req.RequestUri == uri
                    && req.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded"
                );
        }

        [TestMethod]
        public async Task QueryingEmailAddress()
        {
            Mock<HttpMessageHandler> handlerMock = Fakes.HttpMessageHandler();

            var message = new GetEmail(Fakes.TokenId);
            Uri uri;
            Uri.TryCreate(new Uri(LinkedInConstants.DefaultBaseUrl), message.HttpRequestUrl(), out uri);

            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var result = await linkedIn.GetAsync(string.Empty, message, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Fakes.VerifyRequest(
                handlerMock,
                req => req.Method == HttpMethod.Get
                    && req.RequestUri == uri);
        }


        [TestMethod]
        public async Task ApiQueryFailure()
        {
            Mock<HttpMessageHandler> handlerMock = Fakes.HttpMessageHandler(HttpStatusCode.BadRequest, new ErrorResponse()
            {
                Message = "Failed",
                ServiceErrorCode = 7632,
                Status = 401
            });

            var message = new GetEmail(Fakes.TokenId);
            Uri uri;
            Uri.TryCreate(new Uri(LinkedInConstants.DefaultBaseUrl), message.HttpRequestUrl(), out uri);


            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var result = await linkedIn.GetAsync(string.Empty, message, CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            handlerMock.VerifyRequest(req => req.Method == HttpMethod.Get && req.RequestUri == uri);
        }

        [TestMethod]
        public async Task EmailAddressHandler_GET_FromApiEndpoint()
        {
            var handlerMock = Fakes.HttpMessageHandler();

            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var handler = new GetEmailHandler(linkedIn, DummyTokenRegistry.Create()) as ILinkedInRequestHandler<GetEmail, JsonElement>;
            var result = await handler.Handle(new GetEmail(DummyTokenRegistry.ValidTokenId), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);

            Uri uri;
            Uri.TryCreate(new Uri(LinkedInConstants.DefaultBaseUrl), new GetEmail(Fakes.TokenId).HttpRequestUrl(), out uri);

            Fakes.VerifyRequest(
                handlerMock,
                req => req.Method == HttpMethod.Get
                    && req.RequestUri == uri);
        }
    }
}
