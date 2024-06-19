using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IBetService
    {
        public Task<Bet> CreateBetAsync(Bet bet, CancellationToken cancellationToken);
        public Task<Bet?> GetBetByIdAsync(string betId, CancellationToken cancellationToken);
        public Task<IEnumerable<Bet>> GetBetsByClientIdOnPageAsync(string clientId, int page, int amountOnPage, CancellationToken cancellationToken);
    }
}