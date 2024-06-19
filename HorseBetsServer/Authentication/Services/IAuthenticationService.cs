using HorseBets.Api.Authentication.Models;

namespace HorseBets.Api.Authentication.Services
{
    public interface IAuthenticationService
    {
        public Task<bool> CheckRegisteredUserAsync(AuthenticationModelDto loginModel, CancellationToken cancellationToken = default!);
    }
}