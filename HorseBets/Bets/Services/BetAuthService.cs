using Azure.Core;
using HorseBets.Bets.Models.Dto;
using HorseBets.Bets.Services.Api;
using HorseBets.Data;
using Microsoft.AspNetCore.Identity;

namespace HorseBets.Bets.Services
{
    public class BetAuthService : BaseBetApiService, IBetAuthService
    {
        private static bool isProcessing;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccessToken AccessToken
        {
            get => CacheService.BetApiToken;
            private set => CacheService.BetApiToken = value;
        }

        public BetAuthService(IHttpClientFactory httpClientFactory, ILogger<BaseBetApiService> logger,
            UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, logger)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task AuthenticateAsync()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                using (var httpClient = CreateHttpClient())
                {
                    string userId = userManager.GetUserId(user);
                    await SetAuthTokenAsync(httpClient, userId);
                }
            }
            else
            {
                logger.LogWarning("User is not authenticated");
            }
        }
        private async Task SetAuthTokenAsync(HttpClient client, string userId)
        {
            try
            {
                var cachedToken = AccessToken.Token;
                if ((!string.IsNullOrWhiteSpace(cachedToken) && AccessToken.ExpiresOn > DateTime.UtcNow) || isProcessing)
                    return;
                var loginModel = new AuthenticationModelDto
                {
                    UserId = userId,
                };
                isProcessing = true;
                var response = await client.PostAsJsonAsync("auth/login", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await DeserializeResponse(response);
                    CacheService.BetApiToken = responseContent;
                }
                isProcessing = false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                isProcessing = false;
            }
        }

        record class AccessTokenResponse(string Token, DateTimeOffset ExpiresOn);
        private async Task<AccessToken> DeserializeResponse(HttpResponseMessage response)
        {
            var tokenResponse = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
            if (tokenResponse != null)
            {
                return new AccessToken(tokenResponse.Token, tokenResponse.ExpiresOn);
            }
            else
            {
                throw new InvalidOperationException("Deserialization returned null token.");
            }
        }
    }
}
