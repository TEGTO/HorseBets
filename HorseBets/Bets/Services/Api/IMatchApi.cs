using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services.Api
{
    public interface IMatchApi
    {
        public Task<Match?> GetMatchByIdAsync(int matchId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Match>> GetMatchesOnPageAsync(int page, int amountOnPage, bool onlyActive = true, CancellationToken cancellationToken = default);
        public Task<int> GetTotalAmountOfPagesAsync(int amountOnPage, bool onlyActive = true, CancellationToken cancellationToken = default);
        public Task<Match> CreateNewMatchAsync(Match match, CancellationToken cancellationToken = default);
        public Task CancelMatchAsync(Match match, CancellationToken cancellationToken = default);
    }
}