using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public class HorseService(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger) : BaseBetsApi(httpClientFactory, logger), IHorseService
    {
        public async Task<List<Horse>> GetAllHorsesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    List<Horse> horses = await httpClient.GetFromJsonAsync<List<Horse>>($"Horse/horses", cancellationToken);
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
