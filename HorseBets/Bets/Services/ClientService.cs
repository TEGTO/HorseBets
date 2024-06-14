using Fluxor;
using HorseBets.Bets.Services.Api;
using HorseBets.Bets.Store;

namespace HorseBets.Bets.Services
{
    public class ClientService : IClientService
    {
        private IDispatcher dispatcher;
        private IClientApi clientApi;

        public ClientService(IDispatcher dispatcher, IClientApi clientApi)
        {
            this.dispatcher = dispatcher;
            this.clientApi = clientApi;
        }
        public void FetchClientStateByUserId(string userId)
        {
            dispatcher.Dispatch(new FetchClientDataByUserIdAction(userId));
        }
        public async Task CreateClientForUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            await clientApi.CreateClientForUserIdAsync(userId, cancellationToken);
        }
        public void UpdateClientBalance(decimal newBalance)
        {
            dispatcher.Dispatch(new UpdateClientBalanceAction(newBalance));
        }
    }
}
