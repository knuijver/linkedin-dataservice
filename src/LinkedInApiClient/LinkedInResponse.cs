using LinkedInApiClient.Types;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    /// <summary>
    /// A protocol response
    /// </summary>
    public class LinkedInResponse
    {
        public LinkedInResponse()
        {
        }
        public LinkedInResponse(HttpResponseMessage httpResponse)
        {
            this.HttpResponse = httpResponse;
        }

        public string Raw { get; protected set; }

        /// <summary>
        /// Gets the protocol response as JSON (if present).
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        public JsonElement Json { get; protected set; }

        /// <summary>
        /// The actual underling HTTP response message.
        /// </summary>
        public HttpResponseMessage HttpResponse { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether an error occurred.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an error occurred; otherwise, <c>false</c>.
        /// </value>
        public bool IsError => !string.IsNullOrWhiteSpace(Error);

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public ResponseErrorType ErrorType { get; protected set; } = ResponseErrorType.None;

        /// <summary>
        /// Gets or sets an explicit error message.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        protected string ErrorMessage { get; set; }

        /// <summary>
        /// Gets the HTTP status code - or <c>0</c> when <see cref="HttpResponse" /> is <see langword="null"/>.
        /// </summary>
        /// <value>
        /// The HTTP status code.
        /// </value>
        public HttpStatusCode HttpStatusCode => this.HttpResponse?.StatusCode ?? default(HttpStatusCode);

        /// <summary>
        /// Gets the HTTP error reason - or <see langword="null"/> when <see cref="HttpResponse" /> is <see langword="null"/>.
        /// </summary>
        /// <value>
        /// The HTTP error reason.
        /// </value>
        public string HttpErrorReason => this.HttpResponse?.ReasonPhrase ?? default;

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ErrorMessage))
                {
                    return ErrorMessage;
                }
                if (ErrorType == ResponseErrorType.Http)
                {
                    return HttpErrorReason;
                }
                if (ErrorType == ResponseErrorType.Exception)
                {
                    return Exception.Message;
                }

                return string.Empty;
            }
        }

        public T ToObject<T>()
        {
            if (!IsError)
            {
                return JsonSerializer.Deserialize<T>(Raw);
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Initializes a protocol response from an HTTP response
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <returns></returns>
        public static async Task<TResponse> FromHttpResponseAsync<TResponse>(HttpResponseMessage httpResponse, CancellationToken cancellationToken) where TResponse : LinkedInResponse, new()
        {
            var response = new TResponse
            {
                HttpResponse = httpResponse
            };

            string responseContent = default;
            try
            {
                responseContent = await httpResponse.Content
                    .ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);

                response.Raw = responseContent;
            }
            catch
            {
            }

            if (httpResponse.IsSuccessStatusCode == false &&
                httpResponse.StatusCode != HttpStatusCode.BadRequest)
            {
                response.ErrorType = ResponseErrorType.Http;

                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    try
                    {
                        response.Json = JsonDocument.Parse(responseContent).RootElement;
                    }
                    catch { }
                }

                return response;
            }

            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                response.ErrorType = ResponseErrorType.Protocol;
            }

            // either 200 or 400 - both cases need a JSON response (if present), otherwise error
            try
            {
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    response.Json = JsonDocument.Parse(responseContent).RootElement;
                }
            }
            catch (Exception ex)
            {
                response.ErrorType = ResponseErrorType.Exception;
                response.Exception = ex;
            }

            return response;
        }

        /// <summary>
        /// Initializes a protocol response from an exception
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public static LinkedInResponse FromException(Exception ex, string errorMessage = null)
        {
            var response = new LinkedInResponse
            {
                Exception = ex,
                ErrorType = ResponseErrorType.Exception,
                ErrorMessage = errorMessage
            };

            return response;
        }
    }
}
