using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBets.Bets.Models
{
    public class HorseCoefficient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = default!;
        public float Coefficient { get; set; } = 1f;
        public Horse Horse { get; set; } = default!;
        public Match Match { get; set; } = default!;
    }
}
