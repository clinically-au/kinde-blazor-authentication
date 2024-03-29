using System.Security.Claims;
using System.Text.Json;
using Kinde.ManagementApi.Api;
using Kinde.ManagementApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace KindeAuthentication;

public class KindeUserStore(KindeManagementClient client, IHttpContextAccessor httpContextAccessor) : IUserLoginStore<KindeUser>, IUserRoleStore<KindeUser>, IUserClaimStore<KindeUser>
{
    private bool _isDisposed = false;
    
    public void Dispose() => _isDisposed = true;

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, GetType().Name);
    }


    public Task<string> GetUserIdAsync(KindeUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        return Task.FromResult(user.Id);
    }

    public Task<string?> GetUserNameAsync(KindeUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(KindeUser user, string? userName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        user.UserName = userName;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedUserNameAsync(KindeUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Shouldn't be here, as we don't use normalized user names.");
    }

    public Task SetNormalizedUserNameAsync(KindeUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Shouldn't be here, as we don't use normalized user names.");
    }

    public async Task<IdentityResult> CreateAsync(KindeUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentException.ThrowIfNullOrEmpty(user.Email, nameof(user.Email));

        var users = await client.Users.GetUsersAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        var existingUser = users?.Users?.FirstOrDefault(x => x.Email == user.Email)?.Id;

        if (string.IsNullOrEmpty(existingUser))
        {
            var result = await client.Users.CreateUserAsync(
                new CreateUserRequest(
                    new CreateUserRequestProfile(user.GivenName ?? string.Empty, user.FamilyName ?? string.Empty), null!,
                    [
                        new CreateUserRequestIdentitiesInner(
                            CreateUserRequestIdentitiesInner.TypeEnum.Email,
                            new CreateUserRequestIdentitiesInnerDetails(user.Email))
                    ]), cancellationToken).ConfigureAwait(false);
            
            if (!result.Created) return IdentityResult.Failed(new []{new IdentityError(){Code = "CreateUserFailed", Description = "Failed to create user"}});
        }

        return IdentityResult.Success;
    }

    private async Task<bool> AddUserToOrganization(string userId, string orgCode, CancellationToken cancellationToken)
    {
        var response = await client.Organizations.AddOrganizationUsersAsync(
                orgCode,
                new AddOrganizationUsersRequest([new AddOrganizationUsersRequestUsersInner(userId)]), cancellationToken)
            .ConfigureAwait(false);
        
        return response.Code == "200";
    }

    public async Task<IdentityResult> UpdateAsync(KindeUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentException.ThrowIfNullOrEmpty(user.Id, nameof(user.Id));
        await client.Users.UpdateUserAsync(user.Id,
                new UpdateUserRequest(user.GivenName, user.FamilyName), cancellationToken)
            .ConfigureAwait(false);
       return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(KindeUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        var existingUser = await FindByIdAsync(user.Id, cancellationToken);
        ArgumentNullException.ThrowIfNull(existingUser, nameof(existingUser));
        
        if (existingUser.Organizations is not null && existingUser.Organizations.Count > 0)
        {
            throw new Exception("User is a member of one or more organizations and cannot be deleted.");
        }

        await client.Users.DeleteUserAsync(user.Id, cancellationToken: cancellationToken, isDeleteProfile:true).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<KindeUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        var result = await client.Users.GetUsersAsync(userId: userId, expand: "organizations", cancellationToken: cancellationToken).ConfigureAwait(false);
        var first = result.Users.FirstOrDefault();
        return first is null ? null : KindeUser.FromUserResponse(first);
    }

    public async Task<KindeUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        var result = await client.Users.GetUsersAsync(email: normalizedUserName, expand:"organizations", cancellationToken: cancellationToken).ConfigureAwait(false);
        var first = result.Users.FirstOrDefault();
        return first is null ? null : KindeUser.FromUserResponse(first);
    }

    public Task AddLoginAsync(KindeUser user, UserLoginInfo login, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        return Task.FromResult(Task.CompletedTask);
    }

    public Task RemoveLoginAsync(KindeUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        return Task.FromResult(Task.FromResult(Task.CompletedTask));
    }

    public async Task<IList<UserLoginInfo>> GetLoginsAsync(KindeUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        ArgumentNullException.ThrowIfNull(user.Email, nameof(user.Email));
        return new List<UserLoginInfo>()
        {
            new("Kinde", user.Email, "Kinde")
        };
    }

    public async Task<KindeUser?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        return await FindByIdAsync(providerKey, cancellationToken);
    }

    public async Task AddToRoleAsync(KindeUser user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(
            "Kinde requires an organization code to manage user roles. Use the method on KindeUserManager instead.");
    }

    public async Task RemoveFromRoleAsync(KindeUser user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(
            "Kinde requires an organization code to manage user roles. Use the method on KindeUserManager instead.");
    }

    public async Task<IList<string>> GetRolesAsync(KindeUser user, CancellationToken cancellationToken)
    {
        var claims = await GetClaimsAsync(user, cancellationToken).ConfigureAwait(false);
        var roleList = claims.Where(x => x.Type == KindeClaimTypes.Roles)
            .Select(claim => JsonSerializer.Deserialize<KindeRole>(claim.Value)!.Name).ToList();
        return roleList;
    }

    public async Task<bool> IsInRoleAsync(KindeUser user, string roleName, CancellationToken cancellationToken)
    {
        var roles = await GetRolesAsync(user, cancellationToken).ConfigureAwait(false);
        return roles.Contains(roleName);
    }

    public async Task<IList<KindeUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Kinde requires an organization code to get users in a role. Use the method on KindeUserManager instead.");
    }

    public async Task<IList<Claim>> GetClaimsAsync(KindeUser user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));
        var claimsPrincipal = httpContextAccessor.HttpContext?.User;
        ArgumentNullException.ThrowIfNull(claimsPrincipal, nameof(claimsPrincipal));
        
        return claimsPrincipal.Claims.ToList();
    }

    public async Task AddClaimsAsync(KindeUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(
            "Kinde requires an organization code for claim management. Use the Management API Client instead.");
    }

    public async Task ReplaceClaimAsync(KindeUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(
            "Kinde requires an organization code for claim management. Use the Management API Client instead.");
    }

    public async Task RemoveClaimsAsync(KindeUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(
            "Kinde requires an organization code for claim management. Use the Management API Client instead.");
    }

    public async Task<IList<KindeUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(
            "Kinde requires an organization code for claim management. Use the method on KindeUserManager instead.");
    }
}