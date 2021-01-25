using LinkedInApiClient;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.EmailAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClientTests
{
    internal class DummyAccessTokenRegistry : IAccessTokenRegistry
    {
        public Task<Result<string, string>> AccessTokenAsync(string tokenId)
        {
            return Task.FromResult<Result<string, string>>(Result.Success(string.Empty));
        }

        public Task<Result<string, string>> UpdateAccessTokenAsync(string tokenId, string accessToken, string expiresIn, string refreshToken)
        {
            return Task.FromResult<Result<string, string>>(Result.Success(string.Empty));
        }

        public static IAccessTokenRegistry Create() => new DummyAccessTokenRegistry();
    }

    [TestClass]
    public class LinkedInApiRequestUrlTests
    {
        [TestMethod]
        public async Task QueryingEmailAddress()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[]"),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var result = await linkedIn.GetAsync(string.Empty, new GetEmail(string.Empty));

            Assert.IsTrue(result.IsSuccess);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().StartsWith(new GetEmail(string.Empty).Url)),
               ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task ApiQueryFailure()
        {
            var error = new ErrorResponse()
            {
                Message = "Failed",
                ServiceErrorCode = 7632,
                Status = 401
            };
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(error)),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var result = await linkedIn.GetAsync(string.Empty, new GetEmail(string.Empty));


            Assert.IsFalse(result.IsSuccess);

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new GetEmail(string.Empty).HttpRequestUrl()),
               ItExpr.IsAny<CancellationToken>());
        }

        [TestMethod]
        public async Task EmailAddressHandler_GET_FromApiEndpoint()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[]"),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var linkedIn = new LinkedInHttpClient(handlerMock.Object);
            var handler = new GetEmailHandler(linkedIn, DummyAccessTokenRegistry.Create()) as ILinkedInRequestHandler<GetEmail, Option<string>>;
            var result = await handler.Handle(new GetEmail(string.Empty), CancellationToken.None);
            result.Tee(v =>
            {
                Assert.IsNotNull(v);
            });

            result.Match(r => Assert.IsFalse(string.IsNullOrEmpty(r)), () => Assert.Fail());

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().StartsWith(new GetEmail(string.Empty).Url)),
               ItExpr.IsAny<CancellationToken>());

        }
    }
}
