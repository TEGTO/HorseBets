using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services.Api
{
    public interface IClientApi
    {
        public Task<Client> GetClientByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        public Task CreateClientForUserIdAsync(string userId, CancellationToken cancellationToken = default);
    }
}
