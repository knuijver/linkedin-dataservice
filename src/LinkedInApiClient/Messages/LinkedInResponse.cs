using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkedInApiClient.Messages
{
    /// <summary>
    /// A protocol response
    /// </summary>
    public class LinkedInResponse
    {
        public string Raw { get; protected set; }

        public HttpResponseMessage HttpResponse { get; protected set; }

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

                return TryGet(OidcConstants.TokenResponse.Error);
            }
        }


        /// <summary>
        /// Initializes a protocol response from an HTTP response
        /// </summary>
        /// <typeparam name="T">Specific protocol response type</typeparam>
        /// <param name="httpResponse">The HTTP response.</param>
        /// <returns></returns>
        public static Task<T> FromHttpResponseAsync<T>(HttpResponseMessage httpResponse) where T : LinkedInResponse, new()
        {
            T response = new T
            {
                HttpResponse = httpResponse
            };

            string responseContent = default;
            try
            {
                responseContent = await response.Content
                    .ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);

                response.Raw = responseContent;
            }
            catch ()
            {
            }

            if (!httpResponse.IsSuccessStatusCode && httpResponse)
            {
                response.ErrorType = ResponseErrorType.Http;

                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    try
                    {
                        response.Json = JsonDocument.Parse(content).RootElement;
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
                    response.Json = JsonDocument.Parse(content).RootElement;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="ex">The ex.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public static T FromException<T>(Exception ex, string errorMessage = null) where T : LinkedInResponse, new()
        {
            var response = new T
            {
                Exception = ex,
                ErrorType = ResponseErrorType.Exception,
                ErrorMessage = errorMessage
            };

            return response;
        }
    }
}
