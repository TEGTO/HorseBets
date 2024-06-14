using HorseBets.Bets.Models;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public class BetService(IDbContextFactory<BetsDbContext> contextFactory, IClientService clientService) : ServiceDbBase(contextFactory), IBetService
    {
        public async Task<Bet?> GetBetByIdAsync(string betId, CancellationToken cancelentionToken)
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
        public async Task<IEnumerable<Bet>> GetBetsByClientIdOnPageAsync(string clientId, int page, int amountOnPage, CancellationToken cancelentionToken)
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
        public async Task CreateBetAsync(Bet bet, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                Bet newBet = new Bet();
                newBet.CreationTime = DateTime.UtcNow;
                newBet.Client = await dbContext.Clients.FirstAsync(x => x.Id == bet.Client.Id);
                newBet.Match = await dbContext.Matches.FirstAsync(x => x.Id == bet.Match.Id);
                newBet.Horse = await dbContext.Horses.FirstAsync(x => x.Id == bet.Horse.Id);
                await dbContext.Bets.AddAsync(newBet);
                await dbContext.SaveChangesAsync(cancelentionToken);
                await clientService.ReduceValueFromClientBalanceAsync(newBet.Client.Id, newBet.BetAmount, cancelentionToken);
            }
        }
    }
}
