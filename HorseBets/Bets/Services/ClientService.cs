using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;
using HorseBets.Data;

namespace HorseBets.Bets.Services
{
    public class ClientService(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger) : BaseBetsApi(httpClientFactory, logger), IClientService
    {
        public Action<Client> UpdateClientAsync { get; set; }

        public async Task<Client> GetClientByUserAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    Client client = await httpClient.GetFromJsonAsync<Client>($"Client/{user.Id}", cancellationToken);
                    client ??= new Client();
                    return client;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new Client();
            }
        }
        public async Task CreateClientForUserAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    CreateClientDto clientDto = new CreateClientDto() { UserId = user.Id };
                    var result = await httpClient.PostAsJsonAsync($"Client/create", clientDto, cancellationToken);
                    result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
        public async Task AddValueToClientBalanceAsync(Client client, decimal value, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    ChangeClientBalanceDto clientBalanceDto = new ChangeClientBalanceDto()
                    { ClientId = client.Id, Value = value };
                    var result = await httpClient.PatchAsJsonAsync($"Client/add", clientBalanceDto, cancellationToken);
                    result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
