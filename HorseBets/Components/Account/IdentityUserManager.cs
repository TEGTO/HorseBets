using HorseBets.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HorseBets.Components.Account
{
    public class IdentityUserManager : UserManager<ApplicationUser>
    {
        public IdentityUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
        public override async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return await base.IsEmailConfirmedAsync(user) || !Options.SignIn.RequireConfirmedEmail;
        }
    }
}
