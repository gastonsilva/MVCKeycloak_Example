using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Keycloak;
using System;
using System.Configuration;

[assembly: OwinStartup(typeof(MVCKeycloak_Example.Startup))]

namespace MVCKeycloak_Example
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            const string persistentAuthType = "KeycloakOwinAuthenticationSample_cookie_auth";

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = persistentAuthType
            });
            app.SetDefaultSignInAsAuthenticationType(persistentAuthType);

            app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["keycloak-clientid"],
                ClientSecret = ConfigurationManager.AppSettings["keycloak-clientsecret"],
                Realm = ConfigurationManager.AppSettings["keycloak-realm"],
                KeycloakUrl = ConfigurationManager.AppSettings["keycloak-url"],

                VirtualDirectory = "",
                SignInAsAuthenticationType = persistentAuthType,
                AuthenticationType = persistentAuthType,
                DisableAllRefreshTokenValidation = true,
                DisableAudienceValidation = true,
                EnableBearerTokenAuth = true,
                TokenClockSkew = TimeSpan.FromSeconds(2)
            });
        }
    }
}