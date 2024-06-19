using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IClientService
    {
        public Task AddValueToClientBalanceAsync(string clientId, decimal value, CancellationToken cancellationToken);
        public Task<Client> CreateClientForUserIdAsync(string userId, CancellationToken cancellationToken);
        public Task<Client?> GetClientByUserIdAsync(string userId, CancellationToken cancellationToken);
        public Task ReduceValueFromClientBalanceAsync(string clientId, decimal value, CancellationToken cancellationToken);
    }
}