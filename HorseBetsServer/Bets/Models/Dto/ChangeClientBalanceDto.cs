namespace HorseBets.Bets.Models.Dto
{
    public class ChangeClientBalanceDto
    {
        public string ClientId { get; set; } = null!;
        public decimal Value { get; set; }
    }
}
