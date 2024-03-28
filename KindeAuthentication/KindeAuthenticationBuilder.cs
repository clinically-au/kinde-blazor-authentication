using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace KindeAuthentication;

public class KindeAuthenticationOptions()
{
    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string SignedOutRedirectUri { get; set; }

    // For other clients using the API, e.g. mobile apps:
    public bool UseJwkTokenValidation { get; set; } = true; 

    // To avoid keeping any user details client-side; has some down-sides if you have many users
    // as it uses server memory
    public bool UseMemoryCacheTicketStore { get; set; } = false; 
}

public static class KindeAuthenticationBuilder
{
    public static string PermissionsClaimName = "permissions";

    public static AuthenticationBuilder AddKindeAuthentication(this IServiceCollection services, KindeAuthenticationOptions kindeAuthenticationOptions)
    {
        var builder = services.AddAuthentication();
        
        builder.Services.Configure<AuthenticationOptions>(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            options.DefaultSignOutScheme = IdentityConstants.ExternalScheme;
        });
        
        builder.AddOpenIdConnect("OpenIdConnect", options =>
        {
            var authority = kindeAuthenticationOptions.Authority;
            options.Authority = authority;
            options.ClientId = kindeAuthenticationOptions.ClientId;
            options.ClientSecret = kindeAuthenticationOptions.ClientSecret;
            options.SignedOutRedirectUri = kindeAuthenticationOptions.SignedOutRedirectUri;
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.MapInboundClaims = true;
            options.Scope.Add(OpenIdConnectScope.OpenIdProfile);
            options.Scope.Add(OpenIdConnectScope.Email);
            options.Scope.Add("offline"); 
            options.SaveTokens = false;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClaimActions.MapJsonKey(PermissionsClaimName, "permissions");

            if (kindeAuthenticationOptions.UseJwkTokenValidation)
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var client = new HttpClient();
                        var response = client.GetAsync(new Uri($"{authority}/.well-known/jwks")).Result;
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        return JwksHelper.LoadKeysFromJson(responseString);
                    },
                };
            }

            options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated = ctx =>
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadJwtToken(ctx.TokenEndpointResponse.AccessToken);

                        var newClaims = new List<Claim>();
                        // newClaims.AddRange(jsonToken.Claims
                        //     .Where(c => c.Type == "permissions")
                        //     .Select(nc => new Claim(KindeAuthenticationBuilder.PermissionsClaimName, nc.Value)));

                        newClaims.AddRange(jsonToken.Claims
                            .Where(c => c.Type == "roles")
                            .Select(role => new Claim(ClaimTypes.Role,
                                JsonSerializer.Deserialize<KindeRole>(role.Value).key)));
                        
                        newClaims.Add(new Claim(ClaimTypes.GivenName, jsonToken.Claims
                            .FirstOrDefault(c => c.Type == "given_name")?.Value ?? string.Empty));

                        newClaims.Add(new Claim(ClaimTypes.Surname, jsonToken.Claims
                            .FirstOrDefault(c => c.Type == "family_name")?.Value ?? string.Empty));
                        
                        ctx.Principal!.AddIdentity(new ClaimsIdentity(newClaims));
                        return Task.CompletedTask;
                    }
                };
        });
        
        services.AddSingleton<CookieOidcRefresher>();

        if (kindeAuthenticationOptions.UseMemoryCacheTicketStore)
        {
            builder.AddIdentityCookies(bld =>
            {
                bld.ExternalCookie!.PostConfigure(x =>
                    {
                        x.SessionStore = new MemoryCacheTicketStore();
                        x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    })
                    .Configure<CookieOidcRefresher>((cookieOptions, refresher) =>
                    {
                        cookieOptions.Events.OnValidatePrincipal =
                            context => refresher.ValidateOrRefreshCookieAsync(context,
                                IdentityConstants.ExternalScheme);
                    });
            });
        }
        else 
        {
            builder.AddIdentityCookies();
        }

        return builder;
    }
}