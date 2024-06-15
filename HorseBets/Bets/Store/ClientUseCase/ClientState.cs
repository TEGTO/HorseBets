using Fluxor;
using HorseBets.Bets.Models;

namespace HorseBets.Bets.Store.ClientUseCase
{
    [FeatureState]
    public class ClientState
    {
        public Client Client { get; private set; }
        public bool IsLoaded { get; private set; }
        private ClientState()
        {
        }
        public ClientState(Client client, bool isLoading)
        {
            Client = client;
            IsLoaded = isLoading;
        }
    }
}
