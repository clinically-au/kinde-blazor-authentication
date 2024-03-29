using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace KindeAuthentication;

public class AdditionalUserClaimsPrincipalFactory(
    UserManager<KindeUser> userManager,
    RoleManager<KindeRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor,
    IHttpContextAccessor httpContextAccessor)
    : UserClaimsPrincipalFactory<KindeUser, KindeRole>(userManager, roleManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(KindeUser user)
    {
        var principal = await base.CreateAsync(user);
        var identity = (ClaimsIdentity)principal.Identity!;

        if (httpContextAccessor.HttpContext == null) return principal;
        
        var authResult = await httpContextAccessor.HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
        if (!authResult.Succeeded)
        {
            return principal;
        }

        var claims = new List<Claim>();
        
        // Add additional claims here
        if (user.GivenName != null)
        {
            claims.Add(new Claim(ClaimTypes.GivenName, user.GivenName));
        }

        if (user.FamilyName != null)
        {
            claims.Add(new Claim(ClaimTypes.Surname, user.FamilyName));
        }

        if (user.Picture != null)
        {
            claims.Add(new Claim(KindeClaimTypes.Picture, user.Picture));
        }

        var authClaims = authResult.Principal.Claims;

        var authClaimsList = authClaims.ToList();
        
        claims.Add(authClaimsList.First(x => x.Type == KindeClaimTypes.OrganizationCode));
        claims.AddRange(authClaimsList.Where(c => c.Type == KindeClaimTypes.Organizations));
        claims.AddRange(authClaimsList.Where(c => c.Type == KindeClaimTypes.Roles));
        claims.AddRange(authClaimsList.Where(c => c.Type == KindeClaimTypes.Permissions));
        claims.AddRange(authClaimsList.Where(c => c.Type == ClaimTypes.Role));

        identity.AddClaims(claims);

        return principal;
    }
}