using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBets.Bets.Models
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public List<Horse> Participants { get; set; } = new List<Horse>();
        public HorseMatchWinner? Winner { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
