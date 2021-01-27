using System;
using System.Net;
using System.Text.Json.Serialization;

namespace LinkedInApiClient
{
    public class LinkedInError
    {
        public LinkedInError(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}
