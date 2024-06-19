using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services.Api
{
    public class BetApi : BaseBetApiService, IBetApi
    {
        public BetApi(IHttpClientFactory httpClientFactory, ILogger<BaseBetApiService> logger) : base(httpClientFactory, logger)
        {
        }

        public async Task<IEnumerable<Bet>> GetBetsByClientOnPageAsync(Client client, int page, int amountOnPage, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    IEnumerable<Bet>? bets = await
                        httpClient.GetFromJsonAsync<List<Bet>>($"Bet/client?clientId={client.Id}&page={page}&amount={amountOnPage}", cancellationToken);
                    bets ??= new List<Bet>();
                    return bets;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<Bet>();
            }
        }
        public async Task<Bet?> CreateBetAsync(Bet bet, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("Bet/create", bet, cancellationToken);
                    return await response.Content.ReadFromJsonAsync<Bet>(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
