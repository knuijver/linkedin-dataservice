using System;
using System.Linq;

namespace LinkedInApiClient.Messages
{
    public enum ResponseErrorType
    {
        None,
        Protocol,
        Http,
        Exception
    }
}
