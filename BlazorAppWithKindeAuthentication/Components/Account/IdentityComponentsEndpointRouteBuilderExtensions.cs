using System.Security.Claims;
using BlazorAppWithKindeAuthentication.Components.Account.Pages;
using BlazorAppWithKindeAuthentication.Data;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace BlazorAppWithKindeAuthentication.Components.Account;

internal static class IdentityComponentsEndpointRouteBuilderExtensions
{
    // These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var accountGroup = endpoints.MapGroup("/Account");

        accountGroup.MapGet("/Login", (
            HttpContext context,
            [FromServices] SignInManager<ApplicationUser> signInManager,
            [FromQuery] string? returnUrl) =>
        {
            IEnumerable<KeyValuePair<string, StringValues>> query =
            [
                new("ReturnUrl", returnUrl),
                new("Action", ExternalLogin.LoginCallbackAction)
            ];

            var redirectUrl = UriHelper.BuildRelative(
                context.Request.PathBase,
                "/Account/ExternalLogin",
                QueryString.Create(query));

            var properties = signInManager.ConfigureExternalAuthenticationProperties("OpenIdConnect", redirectUrl);
            return TypedResults.Challenge(properties, ["OpenIdConnect"]);
        });

        accountGroup.MapPost("/Logout", async (
            ClaimsPrincipal user,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            [FromForm] string returnUrl) =>
        {
            await signInManager.SignOutAsync();
            var authority = config.GetRequiredSection("Kinde:Authority").Value;
            var baseUrl = config.GetRequiredSection("AppConfig:BaseUrl").Value ?? string.Empty;
            var logoutUri = $"{authority}/logout?redirect={Uri.EscapeDataString(baseUrl)}";
            return TypedResults.Redirect(logoutUri);
        });

        return accountGroup;
    }
}