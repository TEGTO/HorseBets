using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services.Api
{
    public interface IBetApi
    {
        public Task<IEnumerable<Bet>> GetBetsByClientOnPageAsync(Client client, int page, int amountOnPage, CancellationToken cancellationToken = default);
        public Task<Bet?> CreateBetAsync(Bet bet, CancellationToken cancellationToken = default);
    }
}
