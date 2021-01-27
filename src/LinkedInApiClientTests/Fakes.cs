using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
//using Newtonsoft.Json;

namespace LinkedInApiClientTests
{
    internal static class Fakes
    {
        public static readonly string TokenId = Guid.NewGuid().ToString("n");

        public static Mock<HttpMessageHandler> HttpMessageHandler<T>(HttpStatusCode statusCode = HttpStatusCode.OK, T responseContent = default)
        {            
            return HttpMessageHandler(
                statusCode,
                Object.ReferenceEquals(responseContent, default)
                    ? ""
                    : JsonSerializer.Serialize(responseContent)
                    );
        }

        public static Mock<HttpMessageHandler> HttpMessageHandler(HttpStatusCode statusCode = HttpStatusCode.OK, string responseContent = "[]")
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseContent),
            };

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            return handlerMock;
        }

        public static void VerifyRequest(this Mock<HttpMessageHandler> handlerMock, Expression<Func<HttpRequestMessage, bool>> match)
        {
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is(match),
               ItExpr.IsAny<CancellationToken>());
        }
    }
}
