using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IHorseService
    {
        public Task<IEnumerable<Horse>> GetHorsesAsync(CancellationToken cancellationToken);
    }
}