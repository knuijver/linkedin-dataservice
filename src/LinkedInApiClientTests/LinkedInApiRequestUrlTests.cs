using LinkedInApiClient;
using LinkedInApiClient.Authentication;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.EmailAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public async Task RequestAccessToken_Generate_an_Access_Token()
        {
            Mock<HttpMessageHandler> handlerMock = Fakes.HttpMessageHandler(responseContent: new AccessTokenResponse());
            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var clientId = Guid.NewGuid().ToString("n");
            var secret = Convert.ToBase64String(Encoding.UTF8.GetBytes("Keep the Secret"));

            var uri = new Uri(LinkedInConstants.DefaultTokenEndpoint);
            var result = await linkedIn.RequestAccessToken(uri, clientId, secret);

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
            var result = await linkedIn.RequestAccessToken(uri, clientId, secret);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.Error.StatusCode);
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
            Mock<HttpMessageHandler> handlerMock = Fakes.HttpMessageHandler(responseContent: DummyAccessTokenRegistry.RefreshToken);
            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var clientId = Guid.NewGuid().ToString("n");
            var secret = Convert.ToBase64String(Encoding.UTF8.GetBytes("Keep the Secret"));
            var refreshToken = Convert.ToBase64String(Encoding.UTF8.GetBytes("Refresh"));

            var uri = new Uri(LinkedInConstants.DefaultTokenEndpoint);
            var result = await linkedIn.RefreshAccessToken(uri, clientId, secret, refreshToken);

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
            var result = await linkedIn.GetAsync(string.Empty, message);

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
            var result = await linkedIn.GetAsync(string.Empty, message);

            Assert.IsFalse(result.IsSuccess);
            handlerMock.VerifyRequest(req => req.Method == HttpMethod.Get && req.RequestUri == uri);
        }

        [TestMethod]
        public async Task EmailAddressHandler_GET_FromApiEndpoint()
        {
            var handlerMock = Fakes.HttpMessageHandler();

            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var handler = new GetEmailHandler(linkedIn, DummyAccessTokenRegistry.Create()) as ILinkedInRequestHandler<GetEmail, Option<string>>;
            var result = await handler.Handle(new GetEmail(Fakes.TokenId), CancellationToken.None);
            result.Tee(v =>
            {
                Assert.IsNotNull(v);
            });

            result.Match(r => Assert.IsFalse(string.IsNullOrEmpty(r)), () => Assert.Fail());

            Uri uri;
            Uri.TryCreate(new Uri(LinkedInConstants.DefaultBaseUrl), new GetEmail(Fakes.TokenId).HttpRequestUrl(), out uri);

            Fakes.VerifyRequest(
                handlerMock,
                req => req.Method == HttpMethod.Get
                    && req.RequestUri == uri);
        }
    }
}
