using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBets.Bets.Models
{
    public class Bet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        public decimal BetAmount { get; set; }
        public Client Client { get; set; } = null!;
        public Match Match { get; set; } = null!;
        public Horse Horse { get; set; } = null!;
        public DateTime CreationTime { get; set; }
    }
}
