using HorseBets.Api.Authentication.Models;
using HorseBets.Api.Shared.Services;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Api.Authentication.Services
{
    public class AuthenticationService(IDbContextFactory<BetsDbContext> contextFactory) : ServiceDbBase(contextFactory), IAuthenticationService
    {
        public async Task<bool> CheckRegisteredUserAsync(AuthenticationModelDto loginModel, CancellationToken cancellationToken = default!)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                return await dbContext.Clients
                    .AnyAsync(x => x.UserId.Equals(loginModel.UserId), cancellationToken);
            }
        }
    }
}
