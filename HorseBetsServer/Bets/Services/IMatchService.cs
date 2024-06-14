using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IMatchService
    {
        public Task CancelMatchAsync(int matchId, CancellationToken cancelentionToken);
        public Task CreateMatchAsync(Match match, CancellationToken cancelentionToken);
        public Task<Match> GetMatchByIdAsync(int matchId, CancellationToken cancelentionToken);
        public Task<IEnumerable<Match>> GetMatchesOnPageAsync(int page, int amountObjectsPerPage, CancellationToken cancelentionToken, bool onlyActive = true);
        public Task<int> GetPagesAmountAsync(int amountItemsOnPage, CancellationToken cancelentionToken, bool onlyActive = true);
        public Task<int> GetTotalMatchesAsync(CancellationToken cancelentionToken, bool onlyActive = true);
    }
}