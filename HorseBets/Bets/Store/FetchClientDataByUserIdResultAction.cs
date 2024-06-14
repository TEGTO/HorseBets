using HorseBets.Bets.Models;

namespace HorseBets.Bets.Store
{
    public class FetchClientDataByUserIdResultAction
    {
        public Client Client { get; }

        public FetchClientDataByUserIdResultAction(Client client) { Client = client; }
    }
}
