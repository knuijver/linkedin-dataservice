using LinkedInApiClient;
using LinkedInApiClient.Messages;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.People;
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

        //[TestMethod]
        //public async Task IEJ()
        //{
            
        //    var result = await LinkedIn.AccessControl.FindOrganizationAdministrators(
        //        DummyTokenRegistry.ValidTokenId,
        //        CommonURN.OrganizationId("334345345"))
        //        .HandleAsync(DummyTokenRegistry.Create(), new LinkedInHttpClient(), CancellationToken.None);

        //    result
        //        .IfHttpError(err =>
        //        {

        //        })
        //        .IfTokenFailure(err =>
        //        {

        //        })
        //        .IfException(err =>
        //        {

        //        });

        //    if (result.IsSuccess)
        //    {
        //    }
        //}

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

            var message = new GetEmailRequest();
            Uri uri;
            Uri.TryCreate(new Uri(LinkedInConstants.DefaultBaseUrl), message.HttpRequestUrl(), out uri);

            var linkedIn = new HttpClient(handlerMock.Object).UseDefaultLinkedInBaseUrl();
            var result = await linkedIn.GetAsync(message, CancellationToken.None);

            Assert.IsFalse(result.IsError);
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

            var message = new GetEmailRequest();
            Uri uri;
            Uri.TryCreate(new Uri(LinkedInConstants.DefaultBaseUrl), message.HttpRequestUrl(), out uri);


            var linkedIn = new HttpClient(handlerMock.Object).UseDefaultLinkedInBaseUrl();
            var result = await linkedIn.GetAsync(message, CancellationToken.None);

            Assert.IsFalse(result.IsError);
            handlerMock.VerifyRequest(req => req.Method == HttpMethod.Get && req.RequestUri == uri);
        }

        /*
        [TestMethod]
        public async Task EmailAddressHandler_GET_FromApiEndpoint()
        {
            var handlerMock = Fakes.HttpMessageHandler();

            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var handler = new GetEmailHandler(linkedIn, DummyTokenRegistry.Create()) as ILinkedInRequestHandler<GetEmailRequest, JsonElement>;
            var result = await handler.Handle(new GetEmailRequest(DummyTokenRegistry.ValidTokenId), CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);

            Uri uri;
            Uri.TryCreate(new Uri(LinkedInConstants.DefaultBaseUrl), new GetEmailRequest(Fakes.TokenId).HttpRequestUrl(), out uri);

            Fakes.VerifyRequest(
                handlerMock,
                req => req.Method == HttpMethod.Get
                    && req.RequestUri == uri);
        }
        */
    }
}
