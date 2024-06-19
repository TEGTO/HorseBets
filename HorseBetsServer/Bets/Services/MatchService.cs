using HorseBets.Api.Shared.Services;
using HorseBets.Bets.Models;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public class MatchService(IDbContextFactory<BetsDbContext> contextFactory) : ServiceDbBase(contextFactory), IMatchService
    {
        public async Task<Match?> GetMatchByIdAsync(int matchId, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                return await dbContext.Matches
                    .Include(x => x.Participants)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == matchId, cancellationToken);
            }
        }
        public async Task<int> GetPagesAmountAsync(int amountItemsOnPage, CancellationToken cancellationToken, bool onlyActive = true)
        {
            return (int)Math.Ceiling(await GetTotalMatchesAsync(cancellationToken, onlyActive) / (float)amountItemsOnPage);
        }
        public async Task<int> GetTotalMatchesAsync(CancellationToken cancellationToken, bool onlyActive = true)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                return await dbContext.Matches.Where(x => x.IsActive == onlyActive).CountAsync(cancellationToken);
            }
        }
        public async Task<IEnumerable<Match>> GetMatchesOnPageAsync(int page, int amountObjectsPerPage, CancellationToken cancellationToken, bool onlyActive = true)
        {
            IEnumerable<Match> matchesOnPage = new List<Match>();
            if (page > 0 && amountObjectsPerPage > 0)
            {
                using (var dbContext = await CreateDbContextAsync(cancellationToken))
                {
                    if (await GetTotalMatchesAsync(cancellationToken, onlyActive) > 0)
                    {
                        matchesOnPage = await dbContext.Matches
                        .Where(x => x.IsActive == onlyActive)
                        .OrderByDescending(match => match.Id)
                        .Skip((page - 1) * amountObjectsPerPage)
                        .Take(amountObjectsPerPage)
                        .Include(x => x.Participants)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                    }
                }
            }
            return matchesOnPage;
        }
        public async Task<Match> CreateMatchAsync(Match match, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                Match newMatch = new Match();
                newMatch.StartTime = match.StartTime.ToUniversalTime();
                for (int i = 0; i < match.Participants.Count; i++)
                    newMatch.Participants.Add(await dbContext.Horses.FirstAsync(x => x.Id == match.Participants[i].Id));
                await dbContext.AddAsync(newMatch, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                return newMatch;
            }
        }
        public async Task CancelMatchAsync(int matchId, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                Match? matchToCancel = await dbContext.Matches.FirstOrDefaultAsync(x => x.Id == matchId, cancellationToken);
                if (matchToCancel != null)
                {
                    dbContext.Matches.Remove(matchToCancel);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
