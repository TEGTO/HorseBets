using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services.Api
{
    public class HorseApi : BaseBetApiService, IHorseApi
    {
        public HorseApi(IHttpClientFactory httpClientFactory, ILogger<BaseBetApiService> logger) : base(httpClientFactory, logger)
        {
        }

        public async Task<List<Horse>> GetAllHorsesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    List<Horse>? horses = await httpClient.GetFromJsonAsync<List<Horse>>($"Horse/horses", cancellationToken);
                    horses ??= new List<Horse>();
                    return horses;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<Horse>();
            }
        }
    }
}
