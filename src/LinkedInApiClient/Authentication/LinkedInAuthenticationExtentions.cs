using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using LinkedInApiClient.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LinkedInAuthenticationExtentions
    {
        public static AuthenticationBuilder AddOAuth<TOptions, THandler>(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<TOptions> configureOptions)
            where TOptions : OAuthOptions, new()
            where THandler : OAuthHandler<TOptions>
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<TOptions>, OAuthPostConfigureOptions<TOptions, THandler>>());
            return builder.AddRemoteScheme<TOptions, THandler>(authenticationScheme, displayName, configureOptions);
        }

        public static AuthenticationBuilder AddLinkedIn(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<LinkedInOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<LinkedInOptions>, OAuthPostConfigureOptions<LinkedInOptions, LinkedInHandler>>());
            return builder.AddRemoteScheme<LinkedInOptions, LinkedInHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
