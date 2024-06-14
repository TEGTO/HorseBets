using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services.Api
{
    public interface IHorseApi
    {
        public Task<List<Horse>> GetAllHorsesAsync(CancellationToken cancellationToken = default);
    }
}
