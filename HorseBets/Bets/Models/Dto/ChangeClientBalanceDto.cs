namespace HorseBets.Bets.Models.Dto
{
    public class ChangeClientBalanceDto
    {
        public string ClientId { get; set; } = default!;
        public decimal Value { get; set; }
    }
}
