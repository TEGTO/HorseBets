namespace HorseBets.Bets.Models
{
    public class Bet
    {
        public string Id { get; set; }
        public decimal BetAmount { get; set; }
        public Client Client { get; set; } = null!;
        public Match Match { get; set; } = null!;
        public Horse Horse { get; set; } = null!;
    }
}
