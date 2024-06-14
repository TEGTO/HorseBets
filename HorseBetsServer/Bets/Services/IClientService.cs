using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IClientService
    {
        public Task AddValueToClientBalanceAsync(string clientId, decimal value, CancellationToken cancelentionToken);
        public Task<Client> CreateClientForUserIdAsync(string userId, CancellationToken cancelentionToken);
        public Task<Client?> GetClientByUserIdAsync(string userId, CancellationToken cancelentionToken);
        public Task ReduceValueFromClientBalanceAsync(string clientId, decimal value, CancellationToken cancelentionToken);
    }
}