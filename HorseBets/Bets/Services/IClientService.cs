namespace HorseBets.Bets.Services
{
    public interface IClientService
    {
        public void FetchClientStateByUserId(string userId);
        public Task CreateClientForUserIdAsync(string userId, CancellationToken cancellationToken = default);
        public void UpdateClientBalance(decimal newBalance);
    }
}
