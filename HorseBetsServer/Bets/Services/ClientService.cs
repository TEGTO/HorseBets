using HorseBets.Bets.Models;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public class ClientService(IDbContextFactory<BetsDbContext> contextFactory) : ServiceDbBase(contextFactory)
    {
        public async Task<Client?> GetClientByUserIdAsync(string userId, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                return await dbContext.Clients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == userId, cancelentionToken);
            }
        }
        public async Task AddValueToClientBalance(string clientId, decimal value, CancellationToken cancelentionToken)
        {
            if (value >= 0)
                await AddToClientBalance(clientId, value, cancelentionToken);
            else
                throw new InvalidDataException("Value must be greater or equal to 0!");
        }
        public async Task ReduceValueFromClientBalance(string clientId, decimal value, CancellationToken cancelentionToken)
        {
            if (value >= 0)
            {
                value *= -1;
                await AddToClientBalance(clientId, value, cancelentionToken);
            }
            else
                throw new InvalidDataException("Value must be greater or equal to 0!");
        }
        private async Task AddToClientBalance(string clientId, decimal value, CancellationToken cancelentionToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                Client client = await dbContext.Clients.FirstAsync(x => x.Id == clientId, cancelentionToken);
                client.Balance += value;
                await dbContext.SaveChangesAsync(cancelentionToken);
            }
        }
        public async Task CreateClientForUserAsync(string userId, CancellationToken cancelentionToken)
        {
            Client newClient = new Client();
            using (var dbContext = await CreateDbContextAsync(cancelentionToken))
            {
                var user = dbContext.Users.First(x => x.Id == userId);
                newClient.UserId = user.Id;
                await dbContext.AddAsync(newClient, cancelentionToken);
                await dbContext.SaveChangesAsync(cancelentionToken);
            }
        }
    }
}
