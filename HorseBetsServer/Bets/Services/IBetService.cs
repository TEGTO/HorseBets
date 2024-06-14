using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IBetService
    {
        public Task<Bet> CreateBetAsync(Bet bet, CancellationToken cancelentionToken);
        public Task<Bet?> GetBetByIdAsync(string betId, CancellationToken cancelentionToken);
        public Task<IEnumerable<Bet>> GetBetsByClientIdOnPageAsync(string clientId, int page, int amountOnPage, CancellationToken cancelentionToken);
    }
}