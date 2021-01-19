using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public class ErrorResponse
    {
        public int ServiceErrorCode { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class LinkedInError
    {
        public LinkedInError(HttpStatusCode statusCode, string message, int serviceErrorCode)
        {
            this.Message = message;
            this.StatusCode = statusCode;
            ServiceErrorCode = serviceErrorCode;
        }

        public string Message { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public int ServiceErrorCode { get; private set; }

        public static LinkedInError With(HttpStatusCode statusCode, string message)
        {
            return new LinkedInError(statusCode, message, -1);
        }
        public static LinkedInError From(ErrorResponse error)
        {
            return new LinkedInError((HttpStatusCode)error.Status, error.Message, error.ServiceErrorCode);
        }
        /*
                public static LinkedInError ForHttpStatusCode(HttpStatusCode statusCode)
                {
                    var error = statusCode switch
                    {
                        HttpStatusCode.BadRequest => new LinkedInError(statusCode),
                        HttpStatusCode.Unauthorized => new LinkedInError(statusCode),
                        HttpStatusCode.PaymentRequired => new LinkedInError(statusCode),
                        HttpStatusCode.Forbidden => new LinkedInError(statusCode),
                        HttpStatusCode.NotFound => new LinkedInError(statusCode),
                        HttpStatusCode.MethodNotAllowed => new LinkedInError(statusCode),
                        HttpStatusCode.LengthRequired => new LinkedInError(statusCode),
                        HttpStatusCode.TooManyRequests => new LinkedInError(statusCode),
                        HttpStatusCode.InternalServerError => new LinkedInError(statusCode),
                        HttpStatusCode.BadGateway => new LinkedInError(statusCode),
                        HttpStatusCode.ServiceUnavailable => new LinkedInError(statusCode),
                        HttpStatusCode.GatewayTimeout => new LinkedInError(statusCode),

                        _ => throw new NotImplementedException(),
                    };

                    return error;
                }*/
    }
}
