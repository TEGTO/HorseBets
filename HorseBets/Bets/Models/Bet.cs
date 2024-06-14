namespace HorseBets.Bets.Models
{
    public class Bet
    {
        public string Id { get; set; } = string.Empty;
        public decimal BetAmount { get; set; }
        public Client Client { get; set; } = default!;
        public Match Match { get; set; } = default!;
        public Horse Horse { get; set; } = default!;
    }
}
