namespace HorseBets.Bets.Models.Dto
{
    public class MatchDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public List<HorseDto> Participants { get; set; } = new List<HorseDto>();
        public string? WinnerName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
