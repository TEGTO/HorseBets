using HorseBets.Bets.Models;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public class BetService(IDbContextFactory<BetsDbContext> contextFactory, ClientService clientService) : ServiceDbBase(contextFactory)
    {
        public async Task<Bet?> GetBetById(string betId, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                return await dbContext.Bets
                    .Include(x => x.Client)
                    .Include(x => x.Match)
                    .Include(x => x.Horse)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == betId, cancelentionToken);
            }
        }
        public async Task<IEnumerable<Bet>> GetBetsByClientIdOnPage(string clientId, int page, int amountOnPage, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                return await dbContext.Bets
                    .Where(x => x.Client.Id == clientId)
                    .OrderByDescending(bet => bet.CreationTime)
                    .Skip((page - 1) * amountOnPage)
                    .Take(amountOnPage)
                    .Include(x => x.Client)
                    .Include(x => x.Match)
                    .Include(x => x.Horse)
                    .AsNoTracking()
                    .ToArrayAsync(cancelentionToken);
            }
        }
        public async Task CreateBet(Bet bet, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                bet.CreationTime = DateTime.UtcNow;
                bet.Client = await dbContext.Clients.FirstAsync(x => x.Id == bet.Client.Id);
                bet.Match = await dbContext.Matches.FirstAsync(x => x.Id == bet.Match.Id);
                bet.Horse = await dbContext.Horses.FirstAsync(x => x.Id == bet.Horse.Id);
                await dbContext.Bets.AddAsync(bet);
                await dbContext.SaveChangesAsync(cancelentionToken);
                await clientService.ReduceValueFromClientBalance(bet.Client.Id, bet.BetAmount, cancelentionToken);
            }
        }
    }
}
