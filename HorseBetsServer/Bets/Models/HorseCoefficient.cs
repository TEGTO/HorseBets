using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBets.Bets.Models
{
    public class HorseCoefficient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        public float Coefficient { get; set; } = 1f;
        public Horse Horse { get; set; } = null!;
        public Match Match { get; set; } = null!;
    }
}
