using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KindeAuthentication;

public class KindeRoleManager(
    IRoleStore<KindeRole> store,
    IEnumerable<IRoleValidator<KindeRole>> roleValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    ILogger<RoleManager<KindeRole>> logger)
    : RoleManager<KindeRole>(store, roleValidators, keyNormalizer, errors, logger)
{
    
}