using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public interface IHorseService
    {
        public Task<List<Horse>> GetAllHorsesAsync(CancellationToken cancellationToken = default);
    }
}
