using HorseBets.Data;
using Microsoft.AspNetCore.Identity;

namespace HorseBets.Components.Account
{
    public class RoleManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RoleManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public static string[] GetRoles() => Enum.GetNames(typeof(Roles));
        public async Task AddDefaultRoleToUserAsync(ApplicationUser user)
        {
            if (user != null)
                await userManager.AddToRoleAsync(user, Enum.GetName(Roles.Client)!);
        }
    }
}
