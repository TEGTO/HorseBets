using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public abstract class ServiceDbBase
    {
        private readonly IDbContextFactory<BetsDbContext> dbContextFactory;

        public ServiceDbBase(IDbContextFactory<BetsDbContext> contextFactory)
        {
            dbContextFactory = contextFactory;
        }
        protected async Task<BetsDbContext> CreateDbContextAsync(CancellationToken cancelentionToken)
        {
            return await dbContextFactory.CreateDbContextAsync(cancelentionToken);
        }
    }
}
