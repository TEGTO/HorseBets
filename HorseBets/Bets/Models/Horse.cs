namespace HorseBets.Bets.Models
{
    public class Horse
    {
        public string Id { get; set; } = string.Empty;
        public float Speed { get; set; }
        public string? Name { get; set; } = default!;
    }
}
