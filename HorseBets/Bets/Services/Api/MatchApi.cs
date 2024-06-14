using FluentValidation;
using HorseBets.Bets.Models;

namespace HorseBets.Bets.Services.Api
{
    public class MatchApi : BaseBetApiService, IMatchApi
    {
        private readonly IValidator<Match> validator;

        public MatchApi(IHttpClientFactory httpClientFactory, ILogger<MatchApi> logger, IValidator<Match> validator) : base(httpClientFactory, logger)
        {
            this.validator = validator;
        }
        public async Task<Match?> GetMatchByIdAsync(int matchId, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    Match? match = await httpClient.GetFromJsonAsync<Match>($"Match/{matchId}", cancellationToken);
                    return match;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<int> GetTotalAmountOfPagesAsync(int amountOnPage, bool onlyActive = true, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    int totalAmount = await httpClient.GetFromJsonAsync<int>(
                        $"Match/totalPages?amountOnPage={amountOnPage}&onlyActive={onlyActive}",
                        cancellationToken);
                    totalAmount = totalAmount < 0 ? 0 : totalAmount;
                    return totalAmount;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return 0;
            }
        }
        public async Task<IEnumerable<Match>> GetMatchesOnPageAsync(int page, int amountOnPage, bool onlyActive = true, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    IEnumerable<Match>? matches = await httpClient.GetFromJsonAsync<List<Match>>
                        ($"Match/matchesOnPage?page={page}&amountOnPage={amountOnPage}&onlyActive={onlyActive}",
                        cancellationToken);
                    matches ??= new List<Match>();
                    return matches;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<Match>();
            }
        }
        public async Task<Match> CreateNewMatchAsync(Match match, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    await validator.ValidateAsync(match);
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("Match/create", match,
                       cancellationToken);
                    Match? createdMatch = await response.Content.ReadFromJsonAsync<Match>(cancellationToken);
                    createdMatch ??= new Match();
                    return createdMatch;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new Match();
            }
        }
        public async Task CancelMatchAsync(Match match, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    await httpClient.DeleteAsync($"Match/cancel/{match.Id}", cancellationToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
