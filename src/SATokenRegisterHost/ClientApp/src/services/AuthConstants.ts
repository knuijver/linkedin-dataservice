
import { OidcMetadata } from "oidc-client";

export const METADATA_OIDC = (baseUrl: string, issuer?: string): Partial<OidcMetadata> => ({
    issuer: issuer || baseUrl,
    jwks_uri: baseUrl + "/.well-known/openid-configuration/jwks",
    authorization_endpoint: baseUrl + "/connect/authorize",
    token_endpoint: baseUrl + "/connect/token",
    userinfo_endpoint: baseUrl + "/connect/userinfo",
    end_session_endpoint: baseUrl + "/connect/endsession",
    check_session_iframe: baseUrl + "/connect/checksession",
    revocation_endpoint: baseUrl + "/connect/revocation",
    introspection_endpoint: baseUrl + "/connect/introspect"
});

export const APPLICATION_NAME = 'SATokenHost';

export const QueryParameterNames = {
    ReturnUrl: 'returnUrl',
    Message: 'message'
};

export const LogoutActions = {
    LogoutCallback: 'logout-callback',
    Logout: 'logout',
    LoggedOut: 'logged-out'
};

export const LoginActions = {
    Login: 'login',
    LoginCallback: 'login-callback',
    LoginFailed: 'login-failed',
    Profile: 'profile',
    Register: 'register'
};

const prefix = '/authentication';

export const ApplicationPaths = {
    DefaultLoginRedirectPath: '/',
    ApiAuthorizationClientConfigurationUrl: `/_configuration/${APPLICATION_NAME}`,
    ApiAuthorizationPrefix: prefix,
    Login: `${prefix}/${LoginActions.Login}`,
    LoginFailed: `${prefix}/${LoginActions.LoginFailed}`,
    LoginCallback: `${prefix}/${LoginActions.LoginCallback}`,
    Register: `${prefix}/${LoginActions.Register}`,
    Profile: `${prefix}/${LoginActions.Profile}`,
    LogOut: `${prefix}/${LogoutActions.Logout}`,
    LoggedOut: `${prefix}/${LogoutActions.LoggedOut}`,
    LogOutCallback: `${prefix}/${LogoutActions.LogoutCallback}`,
    IdentityRegisterPath: '/Identity/Account/Register',
    IdentityManagePath: '/Identity/Account/Manage'
};
