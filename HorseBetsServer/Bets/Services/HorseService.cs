using HorseBets.Api.Shared.Services;
using HorseBets.Bets.Models;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public class HorseService(IDbContextFactory<BetsDbContext> contextFactory) : ServiceDbBase(contextFactory), IHorseService
    {
        public async Task<IEnumerable<Horse>> GetHorsesAsync(CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                return await dbContext.Horses.AsNoTracking().ToListAsync();
            }
        }
    }
}
