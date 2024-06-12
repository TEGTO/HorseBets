using System.ComponentModel.DataAnnotations;

namespace HorseBets.Bets.Models
{
    public class Match
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public List<Horse> Participants { get; set; } = new List<Horse>();
        public bool IsActive { get; set; } = true;
        public string? Winner { get; set; }
    }
}
