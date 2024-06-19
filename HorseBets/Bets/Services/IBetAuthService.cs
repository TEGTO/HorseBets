using Azure.Core;

namespace HorseBets.Bets.Services
{
    public interface IBetAuthService
    {
        public AccessToken AccessToken { get; }

        public Task AuthenticateAsync();
    }
}