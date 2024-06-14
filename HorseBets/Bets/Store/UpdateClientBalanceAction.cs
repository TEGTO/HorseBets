namespace HorseBets.Bets.Store
{
    public class UpdateClientBalanceAction
    {
        public decimal Balance { get; private set; }

        public UpdateClientBalanceAction(decimal newBalance) { Balance = newBalance; }
    }
}
