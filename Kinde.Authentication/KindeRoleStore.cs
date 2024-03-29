using Kinde.ManagementApi.Api;
using Kinde.ManagementApi.Model;
using Microsoft.AspNetCore.Identity;

namespace KindeAuthentication;

public class KindeRoleStore(KindeManagementClient client) : IRoleStore<KindeRole>
{
    private OrganizationsApi Organizations { get; } = client.GetOrganizationClient().Result;
    private UsersApi Users { get; } = client.GetUserClient().Result;
    private RolesApi Roles { get; } = client.GetRolesClient().Result;

    private bool _isDisposed = false;

    public void Dispose() => _isDisposed = true;

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, GetType().Name);
    }

    public async Task<IdentityResult> CreateAsync(KindeRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        ArgumentNullException.ThrowIfNull(role.Name, nameof(role.Name));
        var result = await Roles.CreateRoleAsync(new CreateRoleRequest(role.Name, role.Description!, role.Key!), cancellationToken);
        return result.Code == "200" ? IdentityResult.Success : IdentityResult.Failed(new []{new IdentityError{Code = result.Code, Description = result.Message}});
    }

    public async Task<IdentityResult> UpdateAsync(KindeRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        ArgumentNullException.ThrowIfNull(role.Name, nameof(role.Name));
        var result = await Roles.UpdateRolesAsync(role.Id, new UpdateRolesRequest(role.Name, role.Description!, role.Key!), cancellationToken);
        return result.Code == "200" ? IdentityResult.Success : IdentityResult.Failed(new []{new IdentityError{Code = result.Code, Description = result.Message}});
    }

    public async Task<IdentityResult> DeleteAsync(KindeRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        ArgumentNullException.ThrowIfNull(role.Id, nameof(role.Id));
        var result = await Roles.DeleteRoleAsync(role.Id, cancellationToken);
        return result.Code == "200" ? IdentityResult.Success : IdentityResult.Failed(new []{new IdentityError{Code = result.Code, Description = result.Message}});
    }

    public Task<string> GetRoleIdAsync(KindeRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        return Task.FromResult(role.Id);
    }

    public Task<string?> GetRoleNameAsync(KindeRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        return Task.FromResult(role.Name)!;
    }

    public Task SetRoleNameAsync(KindeRole role, string? roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role, nameof(role));
        ArgumentNullException.ThrowIfNull(roleName, nameof(roleName));
        role.Name = roleName;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(KindeRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Shouldn't be here, as we don't use normalized role names.");
    }

    public Task SetNormalizedRoleNameAsync(KindeRole role, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Shouldn't be here, as we don't use normalized role names.");
    }

    public async Task<KindeRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        
        var roles = await Roles.GetRolesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        var role = roles.Roles.FirstOrDefault(x => x.Id == roleId);
        return role is null ? null : KindeRole.FromRoles(role);
    }

    public async Task<KindeRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        var roles = await Roles.GetRolesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        var role = roles.Roles.FirstOrDefault<Roles>(x => string.Compare(x.Name,normalizedRoleName, StringComparison.OrdinalIgnoreCase) == 0);
        return role is null ? null : KindeRole.FromRoles(role);
    }
}