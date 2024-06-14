namespace HorseBets.Bets.Models.Dto
{
    public class BetDto
    {
        public string Id { get; set; } = default!;
        public decimal BetAmount { get; set; }
        public ClientDto Client { get; set; } = default!;
        public MatchDto Match { get; set; } = default!;
        public HorseDto Horse { get; set; } = default!;
    }
}
