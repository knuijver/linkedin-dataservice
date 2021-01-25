using LinkedInApiClient;
using LinkedInApiClient.UseCases;
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

            var linkedIn = new LinkedInApiHandler(handlerMock.Object);
            var result = await linkedIn.QueryAsync(string.Empty, new GetEmail());


            Assert.IsTrue(result.IsSuccess);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().StartsWith(new GetEmail().Url)),
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

            var linkedIn = new LinkedInApiHandler(handlerMock.Object);
            var result = await linkedIn.QueryAsync(string.Empty, new GetEmail());


            Assert.IsFalse(result.IsSuccess);

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new GetEmail().HttpRequestUrl()),
               ItExpr.IsAny<CancellationToken>());
        }
    }
}
