using HorseBets.Bets.Models;
using HorseBets.Data;

namespace HorseBets.Bets.Services
{
    public interface IClientService
    {
        public Task<Client> GetClientByUserAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        public Task CreateClientForUserAsync(ApplicationUser user, CancellationToken cancellationToken = default);
        public Action<Client> UpdateClientAsync { get; set; }
    }
}
