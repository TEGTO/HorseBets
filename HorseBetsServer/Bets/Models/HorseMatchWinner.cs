using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBets.Bets.Models
{
    public class HorseMatchWinner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = default!;
        public int MatchId { get; set; }
        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = default!;
        public Horse Horse { get; set; } = default!;
    }
}
