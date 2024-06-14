using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HorseBets.Bets.Models
{
    public class Horse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = default!;
        public float Speed { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public List<Match>? Matches { get; set; }
    }
}
