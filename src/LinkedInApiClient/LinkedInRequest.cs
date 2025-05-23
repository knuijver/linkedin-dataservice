﻿using LinkedInApiClient.Extensions;
using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    /// <summary>
    /// An HttpRequestMessage with HTTP2 set as default and optionally add a Bearer token
    /// </summary>
    public class LinkedInRequest : HttpRequestMessage
    {
        public LinkedInRequest()
        {
            Headers.Accept.Clear();
            Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Version = new Version(2, 0);
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
        }

        public string Address { get; set; }

        public Parameters QueryParameters { get; protected set; } = Parameters.EmptyParameters;

        public string AccessToken { get; set; }

        public RestLiProtocolVersion ProtocolVersion { get; set; } = RestLiProtocolVersion.None;

        public void Prepare()
        {
            if (QueryParameters == null)
            {
                throw new ArgumentNullException(nameof(QueryParameters));
            }

            if (Address.IsMissing())
            {
                throw new ArgumentException(nameof(Address));
            }

            var queryStringInBytes = Encoding.UTF8.GetByteCount(QueryParameters.ToUrlQueryString(string.Empty));
            if (queryStringInBytes >= 4.KiloBytes())
            {
                throw new ArgumentOutOfRangeException(nameof(QueryParameters), "Query string must be less the 4Kb.");
            }

            base.RequestUri = new Uri(QueryParameters.ToUrlQueryString(Address), UriKind.Relative);

            if (AccessToken.IsPresent()) this.SetBearerToken(AccessToken);

            if (ProtocolVersion == RestLiProtocolVersion.V2) Headers.Add("X-Restli-Protocol-Version", "2.0.0");
        }

    }
}
