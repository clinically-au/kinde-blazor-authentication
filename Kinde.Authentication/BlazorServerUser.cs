using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KindeAuthentication;

public class BlazorServerUserAccessor(AuthenticationStateProvider authenticationStateProvider, UserManager<KindeUser> userManager)
{
    public async Task<KindeUser?> GetCurrentUserAsync()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync().ConfigureAwait(false);
        var claimsPrincipal = authState.User;
        return await userManager.GetUserAsync(claimsPrincipal).ConfigureAwait(false);
    }
}