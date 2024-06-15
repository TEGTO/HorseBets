using Fluxor;
using HorseBets.Bets.Services.Api;

namespace HorseBets.Bets.Store.ClientUseCase
{
    public class ClientEffects
    {
        private readonly IClientApi clientApi;
        private readonly ILogger<ClientEffects> logger;

        public ClientEffects(IClientApi clientApi, ILogger<ClientEffects> logger)
        {
            this.clientApi = clientApi;
            this.logger = logger;
        }

        [EffectMethod]
        public async Task HandleFetchClientDataByUserIdAction(FetchClientDataByUserIdAction action, IDispatcher dispatcher)
        {
            var client = await clientApi.GetClientByUserIdAsync(action.userId);
            dispatcher.Dispatch(new FetchClientDataByUserIdResultAction(client));
        }
    }
}
