
using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;

namespace HorseBets.Bets.Services.Api
{
    public class ClientApi : BaseBetApiService, IClientApi
    {
        public ClientApi(IHttpClientFactory httpClientFactory, ILogger<BaseBetApiService> logger) : base(httpClientFactory, logger)
        {
        }

        public async Task<Client> GetClientByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    Client? client = await httpClient.GetFromJsonAsync<Client>($"Client/{userId}", cancellationToken);
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
        public async Task CreateClientForUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    CreateClientDto clientDto = new CreateClientDto() { UserId = userId };
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
