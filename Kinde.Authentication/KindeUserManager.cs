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
    ILogger<UserManager<KindeUser>> logger)
    : UserManager<KindeUser>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
        errors, services, logger)
{
    
};