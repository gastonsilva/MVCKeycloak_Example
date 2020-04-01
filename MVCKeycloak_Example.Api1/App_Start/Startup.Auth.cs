using System.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Owin.Security.Keycloak;

namespace MVCKeycloak_Example.Api1
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            IdentityModelEventSource.ShowPII = true;
            app.UseKeycloakAuthentication(new KeycloakAuthenticationOptions
            {
                Realm = ConfigurationManager.AppSettings["keycloak-realm"],
                ClientId = ConfigurationManager.AppSettings["keycloak-clientid"],
                EnableWebApiMode = true,
                ForceBearerTokenAuth = true,
                KeycloakUrl = ConfigurationManager.AppSettings["keycloak-url"],
                DisableAudienceValidation = true,
                ClientSecret = ConfigurationManager.AppSettings["keycloak-clientsecret"],
                
            });
        }
    }
}
