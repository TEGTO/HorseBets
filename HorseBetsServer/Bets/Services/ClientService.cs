using HorseBets.Api.Shared.Services;
using HorseBets.Bets.Models;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;

namespace HorseBets.Bets.Services
{
    public class ClientService(IDbContextFactory<BetsDbContext> contextFactory) : ServiceDbBase(contextFactory), IClientService
    {
        public async Task<Client?> GetClientByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                return await dbContext.Clients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            }
        }
        public async Task AddValueToClientBalanceAsync(string clientId, decimal value, CancellationToken cancellationToken)
        {
            if (value >= 0)
                await AddToClientBalanceAsync(clientId, value, cancellationToken);
            else
                throw new InvalidDataException("Value must be greater or equal to 0!");
        }
        public async Task ReduceValueFromClientBalanceAsync(string clientId, decimal value, CancellationToken cancellationToken)
        {
            if (value >= 0)
            {
                value *= -1;
                await AddToClientBalanceAsync(clientId, value, cancellationToken);
            }
            else
                throw new InvalidDataException("Value must be greater or equal to 0!");
        }
        private async Task AddToClientBalanceAsync(string clientId, decimal value, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                Client client = await dbContext.Clients.FirstAsync(x => x.Id == clientId, cancellationToken);
                client.Balance += value;
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<Client> CreateClientForUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var dbContext = await CreateDbContextAsync(cancellationToken))
            {
                Client newClient = new Client();
                newClient.UserId = userId;
                await dbContext.AddAsync(newClient, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                return newClient;
            }
        }
    }
}
