using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services
{
    public class BetService(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger, IClientService clientService) : BaseBetsApi(httpClientFactory, logger), IBetService
    {
        public async Task<IEnumerable<Bet>> GetBetsByClientOnPageAsync(Client client, int page, int amountOnPage, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    IEnumerable<Bet> bets;
                    if (client == null)
                        throw new InvalidDataException("Invalid client data!");
                    else
                    {
                        bets = await httpClient.GetFromJsonAsync<List<Bet>>($"Bet/client?clientId={client.Id}&page={page}&amount={amountOnPage}", cancellationToken);
                        bets ??= new List<Bet>();
                    }
                    return bets;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<Bet>();
            }
        }
        public async Task CreateBetAsync(Bet bet, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    if (bet == null || bet.Match == null || bet.Client == null || bet.Horse == null)
                        throw new InvalidDataException("Invalid bet data!");
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("Bet/create", bet, cancellationToken);
                    response.EnsureSuccessStatusCode();
                    bet = await response.Content.ReadFromJsonAsync<Bet>(cancellationToken);
                    clientService.UpdateClientAsync?.Invoke(bet.Client);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
