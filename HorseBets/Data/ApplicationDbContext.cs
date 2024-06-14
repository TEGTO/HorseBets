using HorseBets.Components.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var role in RoleManager.GetRoles())
            {
                builder.Entity<IdentityRole>().HasData(new IdentityRole
                {
                    Name = role,
                    NormalizedName = role.ToUpper(),
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
            }
            base.OnModelCreating(builder);
        }
    }
}