using HorseBets.Bets.Models;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public class MatchService(IDbContextFactory<BetsDbContext> contextFactory) : ServiceDbBase(contextFactory), IMatchService
    {
        public async Task<Match> GetMatchByIdAsync(int matchId, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                return await dbContext.Matches
                    .Include(x => x.Participants)
                    .AsNoTracking()
                    .FirstAsync(x => x.Id == matchId, cancelentionToken);
            }
        }
        public async Task<int> GetPagesAmountAsync(int amountItemsOnPage, CancellationToken cancelentionToken, bool onlyActive = true)
        {
            return (int)Math.Ceiling(await GetTotalMatchesAsync(cancelentionToken, onlyActive) / (float)amountItemsOnPage);
        }
        public async Task<int> GetTotalMatchesAsync(CancellationToken cancelentionToken, bool onlyActive = true)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                return await dbContext.Matches.Where(x => x.IsActive == onlyActive).CountAsync(cancelentionToken);
            }
        }
        public async Task<IEnumerable<Match>> GetMatchesOnPageAsync(int page, int amountObjectsPerPage, CancellationToken cancelentionToken, bool onlyActive = true)
        {
            IEnumerable<Match> matchesOnPage = new List<Match>();
            if (page > 0 && amountObjectsPerPage > 0)
            {
                using (var dbContext = await CreateDbContextAsync(cancelentionToken))
                {
                    if (await GetTotalMatchesAsync(cancelentionToken, onlyActive) > 0)
                    {
                        matchesOnPage = await dbContext.Matches
                        .Where(x => x.IsActive == onlyActive)
                        .OrderByDescending(match => match.Id)
                        .Skip((page - 1) * amountObjectsPerPage)
                        .Take(amountObjectsPerPage)
                        .Include(x => x.Participants)
                        .AsNoTracking()
                        .ToListAsync(cancelentionToken);
                    }
                }
            }
            return matchesOnPage;
        }
        public async Task CreateMatchAsync(Match match, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                Match newMatch = new Match();
                newMatch.StartTime = match.StartTime.ToUniversalTime();
                for (int i = 0; i < match.Participants.Count; i++)
                    newMatch.Participants.Add(await dbContext.Horses.FirstAsync(x => x.Id == match.Participants[i].Id));
                await dbContext.AddAsync(newMatch, cancelentionToken);
                await dbContext.SaveChangesAsync(cancelentionToken);
            }
        }
        public async Task CancelMatchAsync(int matchId, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                Match? matchToCancel = await dbContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId, cancelentionToken);
                if (matchToCancel != null)
                {
                    dbContext.Matches.Remove(matchToCancel);
                    await dbContext.SaveChangesAsync(cancelentionToken);
                }
            }
        }
    }
}
