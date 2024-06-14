using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBets.Bets.Models
{
    public class Bet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = default!;
        public decimal BetAmount { get; set; }
        public Client Client { get; set; } = default!;
        public Match Match { get; set; } = default!;
        public Horse Horse { get; set; } = default!;
        public DateTime CreationTime { get; set; }
    }
}
