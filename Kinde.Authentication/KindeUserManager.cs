using Kinde.ManagementApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KindeAuthentication;

public class KindeUserManager(
    IUserStore<KindeUser> store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<KindeUser> passwordHasher,
    IEnumerable<IUserValidator<KindeUser>> userValidators,
    IEnumerable<IPasswordValidator<KindeUser>> passwordValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<UserManager<KindeUser>> logger,
    KindeManagementClient client)
    : UserManager<KindeUser>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
        errors, services, logger)
{
    public async Task<IList<KindeUser>> GetUsersInRoleAsync(string roleName, string orgCode, CancellationToken cancellationToken) 
    {
        var response = await client.Organizations.GetOrganizationUsersAsync(orgCode, roles: roleName , cancellationToken:cancellationToken).ConfigureAwait(false);
        return response.OrganizationUsers.Select(KindeUser.FromOrganizationUser).ToList();
    }

    public async Task AddToRoleAsync(KindeUser user, string roleName, string orgCode, CancellationToken cancellationToken)
    {
        var roles = (await client.Roles.GetRolesAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).Roles;
        var roleId = roles.FirstOrDefault(x => x.Name == roleName)?.Id;
        if (string.IsNullOrEmpty(roleId)) throw new InvalidOperationException("Role not found");
        
        var result = await client.Organizations.CreateOrganizationUserRoleAsync(orgCode, user.Id, new CreateOrganizationUserRoleRequest(roleId), cancellationToken).ConfigureAwait(false);
        if (result.Code != "200") throw new InvalidOperationException(result.Message);
    }

    public async Task RemoveFromRoleAsync(KindeUser user, string roleName, string orgCode, CancellationToken cancellationToken)
    {
        var roles = (await client.Roles.GetRolesAsync(cancellationToken: cancellationToken).ConfigureAwait(false)).Roles;
        var roleId = roles.FirstOrDefault(x => x.Name == roleName)?.Id;
        if (string.IsNullOrEmpty(roleId)) throw new InvalidOperationException("Role not found");
        
        var result = await client.Organizations.DeleteOrganizationUserRoleAsync(orgCode, user.Id, roleId, cancellationToken).ConfigureAwait(false);
        if (result.Code != "200") throw new InvalidOperationException(result.Message);
    }

    public async Task<IList<KindeUser>> GetUsersForPermission(string permissionName, string orgCode, CancellationToken cancellationToken)
    {
        var users = await client.Organizations.GetOrganizationUsersAsync(orgCode, permissions: permissionName, cancellationToken: cancellationToken).ConfigureAwait(false);
        return users.OrganizationUsers.Select(KindeUser.FromOrganizationUser).ToList();
    }

    public async Task<bool> HasPermission(KindeUser user, string permission, string orgCode,
        CancellationToken cancellationToken)
    {
        var claims = await GetClaimsAsync(user).ConfigureAwait(false);
        return claims.Any(x => x.Value == permission);
    }
};