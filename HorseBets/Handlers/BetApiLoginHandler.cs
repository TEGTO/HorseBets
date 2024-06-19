using HorseBets.Bets.Services;
using HorseBets.Data;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;

namespace HorseBets.Handlers
{
    public class BetApiLoginHandler : DelegatingHandler
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBetAuthService betAuthService;
        private readonly ILogger<BetApiLoginHandler> logger;

        public BetApiLoginHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IBetAuthService betAuthService, ILogger<BetApiLoginHandler> logger)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.betAuthService = betAuthService;
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await betAuthService.AuthenticateAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", betAuthService.AccessToken.Token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
