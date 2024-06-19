using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IMatchService
    {
        public Task CancelMatchAsync(int matchId, CancellationToken cancellationToken);
        public Task<Match> CreateMatchAsync(Match match, CancellationToken cancellationToken);
        public Task<Match?> GetMatchByIdAsync(int matchId, CancellationToken cancellationToken);
        public Task<IEnumerable<Match>> GetMatchesOnPageAsync(int page, int amountObjectsPerPage, CancellationToken cancellationToken, bool onlyActive = true);
        public Task<int> GetPagesAmountAsync(int amountItemsOnPage, CancellationToken cancellationToken, bool onlyActive = true);
        public Task<int> GetTotalMatchesAsync(CancellationToken cancellationToken, bool onlyActive = true);
    }
}