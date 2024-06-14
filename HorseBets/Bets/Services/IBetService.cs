using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IBetService
    {
        public Task<IEnumerable<Bet>> GetBetsByClientOnPageAsync(Client client, int page, int amountOnPage, CancellationToken cancellationToken = default);
        public Task CreateBetAsync(Bet bet, CancellationToken cancellationToken = default);
    }
}