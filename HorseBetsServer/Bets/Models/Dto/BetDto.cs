namespace HorseBets.Bets.Models.Dto
{
    public class BetDto
    {
        public string Id { get; set; } = null!;
        public decimal BetAmount { get; set; }
        public ClientDto Client { get; set; } = null!;
        public MatchDto Match { get; set; } = null!;
        public HorseDto Horse { get; set; } = null!;
    }
}
