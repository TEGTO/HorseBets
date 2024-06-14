namespace HorseBets.Bets.Store
{
    public class FetchClientDataByUserIdAction
    {
        public string userId { get; }

        public FetchClientDataByUserIdAction(string userId)
        {
            this.userId = userId;
        }
    }
}
