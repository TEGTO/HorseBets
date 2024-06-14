namespace HorseBets.Bets.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public List<Horse> Participants { get; set; } = new List<Horse>();
        public bool IsActive { get; set; } = true;
        public string? Winner { get; set; } = null;
    }
}
