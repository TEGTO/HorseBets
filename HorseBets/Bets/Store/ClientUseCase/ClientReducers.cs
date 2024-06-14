using Fluxor;
using HorseBets.Bets.Models;

namespace HorseBets.Bets.Store.ClientUseCase
{
	public class ClientReducers
	{
		[ReducerMethod]
		public static ClientState ReduceFetchClientDataAction(ClientState state, FetchClientDataByUserIdAction action)
		{
			return new ClientState(client: new Client() { Balance = 1000 });
		}
		[ReducerMethod]
		public static ClientState ReduceFetchClientDataByUserIdResultAction(ClientState state, FetchClientDataByUserIdResultAction action) =>
			  new ClientState(action.Client);
		[ReducerMethod]
		public static ClientState ReduceUpdateClientBalance(ClientState state, UpdateClientBalanceAction action)
			=> new ClientState(new Client() { Id = state.Client.Id, Balance = action.Balance });
	}
}
